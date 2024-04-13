using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TuristikFirma.Abstractions;
using TuristikFirma.Contracts;
using TuristikFirma.Models;
using TuristikFirma.Services;

namespace TuristikFirma.Controllers
{
    [Route("api/tours")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly IToursService _toursService;
        private readonly IHelperService _helperService;

        public ToursController(IToursService toursService, IHelperService helperService) 
        {
            _toursService = toursService;
            _helperService = helperService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ToursResponse>>> GetTours()
        {
            var tours = await _toursService.GetAllTours();

            var response = tours.Select(b => new ToursResponse(b.Id, b.TitleEn, b.TitleKz, 
                                                               b.TitleRu, b.DescriptionEn, b.DescriptionKz, 
                                                               b.DescriptionRu, b.Price, b.PreviewPhotoPath, 
                                                               b.Country));

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<List<ToursResponse>>> GetTour(Guid id)
        {
            var b = await _toursService.GetOneTour(id);


            var response = new ToursResponse(b.Id, b.TitleEn, b.TitleKz,
                                                               b.TitleRu, b.DescriptionEn, b.DescriptionKz,
                                                               b.DescriptionRu, b.Price, b.PreviewPhotoPath,
                                                               b.Country);

            return Ok(response);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize("OnlyAdmin")]
        public async Task<ActionResult<Guid>> CreateTour([FromBody] ToursRequest request)
        {
            var (tour, error) = Tour.Create(
                Guid.NewGuid(),
                request.TitleEn,
                request.TitleKz,
                request.TitleRu,
                request.DescriptionEn,
                request.DescriptionKz,
                request.DescriptionRu,
                request.Price,
                request.PreviewPhotoPath,
                request.Country
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var tourId = await _toursService.CreateTour(tour);

            return Ok(tourId);
        }

        [HttpPut("{id:guid}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize("OnlyAdmin")]
        public async Task<ActionResult<Guid>> UpdatePost(Guid id, [FromBody] ToursRequest request)
        {
            var bookId = await _toursService.UpdateTour(id, request.TitleEn,
                request.TitleKz,
                request.TitleRu,
                request.DescriptionEn,
                request.DescriptionKz,
                request.DescriptionRu,
                request.Price,
                request.PreviewPhotoPath,
                request.Country);

            return Ok(bookId);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize("OnlyAdmin")]
        public async Task<ActionResult<Guid>> DeleteTour(Guid id)
        {
            return Ok(await _toursService.DeleteTour(id));
        }

        [HttpPost("uploadImage")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize("OnlyAdmin")]
        public async Task<ActionResult<string>> UploadTourImage(IFormFile file)
        {
            string result = await _helperService.WriteFile(file, "posts", $"{DateTime.Now.Ticks}");

            return Ok(String.Join("/", result.Split('\\')));
        }
    }
}

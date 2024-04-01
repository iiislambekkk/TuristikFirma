using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Security.Claims;
using TuristikFirma.Abstractions;
using TuristikFirma.Models;

//User123@example.com
namespace TuristikFirma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IHelperService _helperService;
        private readonly UserManager<User> _userManager;

        public UsersController(IHelperService helperService, UserManager<User> user) 
        {
            _helperService = helperService;
            _userManager = user;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("uploadAvatar")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {

            User user = await _userManager.FindByEmailAsync(User.Identity.Name);
            

            if (user == null) 
            {
                return BadRequest();
            }

            string result = await _helperService.WriteFile(file, "avatars", $"{user.Id}");

            return Ok(String.Join("/", result.Split('\\')));
        }
    }
}

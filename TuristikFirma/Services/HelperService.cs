using System.ComponentModel;
using TuristikFirma.Abstractions;

namespace TuristikFirma.Services
{
    public class HelperService : IHelperService
    {
        public HelperService() { }

        public async Task<string> WriteFile(IFormFile file, string directoryPath, string fileName)
        {
            string fileNameWithExt = fileName;
            try
            {
                
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileNameWithExt += extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{directoryPath}");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{directoryPath}", fileNameWithExt);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
            }
            
            return Path.Combine($"{directoryPath}", fileNameWithExt);
        }

        public string haha()
        {
            return "dsadasd";
        }
    }
}

namespace TuristikFirma.Abstractions
{
    public interface IHelperService
    {
        Task<string> WriteFile(IFormFile file, string directoryPath, string fileName);

        string haha();
    }
}
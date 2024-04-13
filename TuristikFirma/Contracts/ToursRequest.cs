namespace TuristikFirma.Contracts
{
    public record ToursRequest(
        string TitleEn, 
        string TitleKz, 
        string TitleRu, 
        string DescriptionEn, 
        string DescriptionKz, 
        string DescriptionRu, 
        decimal Price, 
        string PreviewPhotoPath, 
        string Country);
}

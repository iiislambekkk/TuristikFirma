namespace TuristikFirma.Contracts
{
    public record ToursResponse(
        Guid Id,
        string TitleEn,
        string TitleKz,
        string TitleRu,
        string DescriptionEn,
        string DescriptionKz,
        string DescriptionRu,
        decimal Price,
        string PreviewPhotoPath,
        string Country,
        string DaysEn,
        string DaysKz,
        string DaysRu,
        int NumOfDays
        );
}

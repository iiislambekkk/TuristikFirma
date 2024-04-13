namespace TuristikFirma.TuristikFirma.DataAccess.Entities
{
    public class TourEntity
    {
        public Guid Id { get; set; }

        public string TitleEn { get; set; } = string.Empty;
        public string TitleKz { get; set; } = string.Empty;
        public string TitleRu { get; set; } = string.Empty;

        public string DescriptionEn { get; set; } = string.Empty;
        public string DescriptionKz { get; set; } = string.Empty;
        public string DescriptionRu { get; set; } = string.Empty;

        public string PreviewPhotoPath { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public string DaysEn {  get; set; } = string.Empty;
        public string DaysKz {  get; set; } = string.Empty;
        public string DaysRu {  get; set; } = string.Empty;
        public int NumOfDays { get; set; } = 0;
    }
}

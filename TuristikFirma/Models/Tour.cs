namespace TuristikFirma.Models
{
    public class Tour
    {
        public const int MAX_TITLE_LENGTH = 250;

        private Tour(Guid id, string titleEn, string titleKz, string titleRu, string descriptionEn, string descriptionKz, string descriptionRu, decimal price, string previewPhotoPath, string country)
        {
            Id = id;

            TitleEn = titleEn;
            TitleKz = titleKz;
            TitleRu = titleRu;

            DescriptionEn = descriptionEn;
            DescriptionKz = descriptionKz;
            DescriptionRu = descriptionRu;

            PreviewPhotoPath = previewPhotoPath;
            Country = country;

            Price = price;
        }

        public Guid Id { get; }

        public string TitleEn { get; } = string.Empty;
        public string TitleKz { get; } = string.Empty;
        public string TitleRu { get; } = string.Empty;

        public string DescriptionEn { get; } = string.Empty;
        public string DescriptionKz { get; } = string.Empty;
        public string DescriptionRu { get; } = string.Empty;

        public string? PreviewPhotoPath { get; } = string.Empty;
        public string Country { get; } = string.Empty;
        public decimal Price { get; }

        public static (Tour Post, string Error) Create(Guid id, string titleEn, string titleKz, string titleRu, string descriptionEn, string descriptionKz, string descriptionRu, decimal price, string previewPhotoPath, string country)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(titleEn) || titleEn.Length > MAX_TITLE_LENGTH)
            {
                error = $"Title cannot be empty or longer than {MAX_TITLE_LENGTH} symbols";
            }

            var book = new Tour(id, titleEn, titleKz, titleRu, descriptionEn, descriptionKz, descriptionRu, price, previewPhotoPath, country);

            return (book, error);
        }
    }
}

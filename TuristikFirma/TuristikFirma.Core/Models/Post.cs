namespace TuristikFirma.TuristikFirma.Core.Models
{
    public class Post
    {
        public const int MAX_TITLE_LENGTH = 255;

        private Post(Guid id, string title, string description, decimal price)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
        }

        public Guid Id { get; }

        public string Title { get; } = string.Empty;

        public string Description { get; } = string.Empty;

        public decimal Price { get; }

        public static (Post Post, string Error) Create(Guid id, string title, string description, decimal price)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            {
                error = $"Title cannot be empty or longer than {MAX_TITLE_LENGTH} symbols";
            }

            var book = new Post(id, title, description, price);

            return (book, error);
        }
    }
}

namespace TuristikFirma.Models
{
    public class Comment
    {
        private Comment(Guid id, string text, string date, Guid entityId, Guid userId, Guid parentId)
        {
            Id = id;
            Text = text;
            Date = date;
            EntityId = entityId;
            UserId = userId;
            ParentId = parentId;
        }

        public Guid Id { get; }
        public string Text { get; }
        public string Date { get; }
        public Guid EntityId { get; }
        public Guid UserId { get; }
        public Guid ParentId { get; } = Guid.Empty;

        public static (Comment Comment, string error) Create(Guid id, string text, string date, Guid entityId, Guid userId, Guid parentId)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(text))
            {
                error = $"Text of comment cannot be empty.";
            }

            var comment = new Comment(id, text, date, entityId, userId, parentId);

            return (comment, error);
        }
    }
}

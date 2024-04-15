namespace TuristikFirma.DbContext.Entities
{
    public class CommentEntity
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public Guid EntityId { get; set; }
        public Guid UserId { get; set; }
        public Guid ParentId { get; set; }
    }
}

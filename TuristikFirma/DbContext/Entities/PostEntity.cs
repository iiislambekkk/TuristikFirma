namespace TuristikFirma.TuristikFirma.DataAccess.Entities
{
    public class PostEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}

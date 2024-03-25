namespace TuristikFirma.Contracts
{
    public record PostsRequest(
        string Title,
        string Description,
        decimal Price);
}

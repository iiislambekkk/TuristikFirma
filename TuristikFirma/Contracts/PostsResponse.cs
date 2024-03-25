namespace TuristikFirma.Contracts
{
    public record PostsResponse(
        Guid Id,
        string Title,
        string Description,
        decimal Price);
}

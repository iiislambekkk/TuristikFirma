namespace TuristikFirma.Contracts
{
    public record CommentsResponse(
       Guid Id,
       string Text,
       string Date,
       Guid EntityId,
       Guid UserId,
       Guid ParentId
    );
}

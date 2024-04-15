namespace TuristikFirma.Contracts
{
    public record CommentsRequest(
       string Text,
       string Date,
       Guid EntityId,
       Guid UserId,
       Guid ParentId
    );
}

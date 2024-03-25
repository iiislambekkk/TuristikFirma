using Microsoft.AspNetCore.Identity;

namespace TuristikFirma.Contracts
{
    public record LoginResponse
    (
        string token,
        string role
    );
}

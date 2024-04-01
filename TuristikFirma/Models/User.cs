using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace TuristikFirma.Models
{
    public class User : IdentityUser
    {
        [AllowNull]
        public string avatarPath { get; set; }
    }
}

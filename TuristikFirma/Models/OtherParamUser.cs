using System.ComponentModel.DataAnnotations;

namespace TuristikFirma.Models
{
    public class OtherParamUser : ParamUser
    {
        public string UserName { get; set; }

        [Required]
        public enumRoles Role { get; set; }

        public string? avatarPath { get; set; } = " ";
    }
}

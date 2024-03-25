using System.ComponentModel.DataAnnotations;

namespace TuristikFirma.TuristikFirma.Core.Models
{
    public class OtherParamUser : ParamUser
    {
        public string UserName { get; set; }

        [Required]
        public enumRoles Role { get; set; }
    }
}

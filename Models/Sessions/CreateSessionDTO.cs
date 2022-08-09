using System.ComponentModel.DataAnnotations;
using UserApi.Data.Enum;

namespace UserApi.Models.Sessions
{
    public class CreateSessionDTO
    {
        [Required]
        public SessionType Type { get; set; }
    }
}

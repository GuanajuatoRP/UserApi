using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.Sessions
{
    public class AddUserInSessionDTO
    {
        [Required]
        public string username { get; set; }
    }
}

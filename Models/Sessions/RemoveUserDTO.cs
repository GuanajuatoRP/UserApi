using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.Sessions
{
    public class RemoveUserDTO
    {
        [Required]
        public string DiscordId { get; set; }
    }
}

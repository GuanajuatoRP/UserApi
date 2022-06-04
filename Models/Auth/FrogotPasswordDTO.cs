using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.Auth
{
    public class FrogotPasswordDTO
    {
        [Required]
        public string DiscordId { get; set; }
    }
}

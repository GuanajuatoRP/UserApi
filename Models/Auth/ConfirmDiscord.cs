using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.Auth
{
    public class ConfirmDiscordDTO
    {
        [Required]
        public string DiscordId { get; set; }
        [Required]
        public string ConfirmationToken { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

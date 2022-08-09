using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.Auth
{
    public class ValidationRegistrationDTO
    {
        [Required]
        public string discordId { get; set; }
        [Required]
        public string token { get; set; }
    }
}

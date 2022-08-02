using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.Auth
{
    public class RegisterDTO
    {
        [Required]
        public string DiscordId { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Sexe { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

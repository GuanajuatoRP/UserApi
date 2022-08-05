using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.Auth
{
    public class userValidatedOnDbDTO
    {
        [Required]
        public string userId { get; set; }
        [Required]
        public string discordId { get; set; }
    }
}

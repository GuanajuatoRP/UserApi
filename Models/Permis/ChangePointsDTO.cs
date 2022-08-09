using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.Permis
{
    public class ChangePointsDTO
    {
        [Required]
        public int Value { get; set; }
    }
}

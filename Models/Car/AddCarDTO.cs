using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.Car
{
    public class AddCarDTO
    {
        [Required]
        public Guid CarId { get; set; }
    }
}

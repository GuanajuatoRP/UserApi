using System.ComponentModel.DataAnnotations;
using UserApi.Data.Enum;

namespace UserApi.Models.Garage
{
    public class EditCarDTO
    {
        [Required]
        public Guid? KeyCar { get; set; }
        [Required]
        public int PowerHp { get; set; }
        [Required]
        public decimal WeightKg { get; set; }
        [Required]
        public string DriveTrain { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public int Pi { get; set; }
        [Required]
        public bool OnRoad { get; set; }
        [Required]
        public decimal Speed { get; set; }
        [Required]
        public decimal Handling { get; set; }
        [Required]
        public decimal Accelerate { get; set; }
        [Required]
        public decimal Launch { get; set; }
        [Required]
        public decimal Braking { get; set; }
        [Required]
        public decimal Offroad { get; set; }
        public string? Imatriculation { get; set; }
        [Required]
        public int TotalPrice { get; set; }
        [Required]
        public int? EditPrice { get; set; }
    }
}

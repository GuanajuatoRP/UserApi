using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.Car
{
    public class CreateOriginalCarDTO
    {
        [Required]
        public int CarId { get; set; }
        [Required]
        public int CarOrdinal { get; set; }
        [Required]
        public string Maker { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
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
        [Required]
        public bool RequiredDlc { get; set; }
        [Required]
        public string Aviability { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Rarity { get; set; }
        [Required]
        public string WikiLink { get; set; }
        [Required]
        public string PictureLink { get; set; }
    }
}

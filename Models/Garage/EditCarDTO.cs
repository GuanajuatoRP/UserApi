using System.ComponentModel.DataAnnotations;
using UserApi.Data.Enum;

namespace UserApi.Models.Garage
{
    public class EditCarDTO
    {
        [Required]
        public Guid? KeyCar { get; set; }
        [Required]
        public int PowerBHP { get; set; }
        [Required]
        public int PowerKW { get; set; }
        [Required]
        public int TorqueLBFT { get; set; }
        [Required]
        public int TorqueNM { get; set; }
        [Required]
        public int WeightLBS { get; set; }
        [Required]
        public string Transmission { get; set; }
        [Required]
        public int GearBox { get; set; }
        [Required]
        public int WeightKG { get; set; }
        [Required]
        public decimal EngineDisplacement { get; set; }
        [Required]
        public int NbCylindre { get; set; }
        [Required]
        public string EnginePosition { get; set; }
        [Required]
        public int PrixTotal { get; set; }
        [Required]
        public int PrixModif { get; set; }
        [Required]
        public string Aspiration { get; set; }
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
        public int Pi { get; set; }
        [Required]
        public string Class { get; set; }
        //[Required]
        public string Imatriculation { get; set; }
    }
}

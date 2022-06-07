using System.ComponentModel.DataAnnotations;
using UserApi.Data.Enum;

namespace UserApi.Models.Garage
{
    public class EditCarDTO
    {
        [Required]
        public Guid? KeyCar { get; set; }
        [Required]
        public int Power_BHP { get; set; }
        [Required]
        public int Power_KW { get; set; }
        [Required]
        public int Torque_LBFT { get; set; }
        [Required]
        public int Torque_NM { get; set; }
        [Required]
        public int Weight_LBS { get; set; }
        [Required]
        public int Weight_KG { get; set; }
        [Required]
        public decimal EngineDisplacement { get; set; }
        [Required]
        public int NbCylindre { get; set; }
        [Required]
        public EnginePosition EnginePosition { get; set; }
        [Required]
        public int PrixTotal { get; set; }
        [Required]
        public int PrixModif { get; set; }
        [Required]
        public Aspiration Aspiration { get; set; }
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
        public Class Class { get; set; }
        [Required]
        public string Imatriculation { get; set; }
    }
}

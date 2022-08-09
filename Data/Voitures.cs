using System.ComponentModel.DataAnnotations;
using UserApi.Data.Enum;

namespace UserApi.Data
{
    public class Voitures
    {
        [Key]
        public Guid KeyCar { get; set; }
        public string IdUser { get; set; }
        public ApiUser User { get; set; }
        public Guid IdCar { get; set; }
        public decimal PowerBHP { get; set; }
        public decimal PowerKW { get; set; }
        public decimal TorqueLBFT { get; set; }
        public decimal TorqueNM { get; set; }
        public decimal WeightLBS { get; set; }
        public decimal WeightKG { get; set; }
        public decimal EngineDisplacement { get; set; }
        public int NbCylindre { get; set; }
        public EnginePosition EnginePosition { get; set; }
        public int PrixTotal { get; set; }
        public int PrixModif { get; set; }
        public Aspiration Aspiration { get; set; }
        public string Transmission { get; set; }
        public int GearBox { get; set; }
        public decimal Speed { get; set; }
        public decimal Handling { get; set; }
        public decimal Accelerate { get; set; }
        public decimal Launch { get; set; }
        public decimal Braking { get; set; }
        public decimal Offroad { get; set; }
        public int Pi { get; set; }
        public Class Class { get; set; }
        public string Imatriculation { get; set; }

    }
}

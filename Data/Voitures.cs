using System.ComponentModel.DataAnnotations;
using UserApi.Data.Enum;

namespace UserApi.Data
{
    public class Voitures
    {
        [Key]
        public Guid? KeyCar { get; set; }
        public string IdUser { get; set; }
        public ApiUser User { get; set; }
        public Guid IdCar { get; set; }
        public int Power_BHP { get; set; }
        public int Power_KW { get; set; }
        public int Torque_LBFT { get; set; }
        public int Torque_NM { get; set; }
        public int Weight_LBS { get; set; }
        public int Weight_KG { get; set; }
        public decimal EngineDisplacement { get; set; }
        public int NbCylindre { get; set; }
        public EnginePosition EnginePosition { get; set; }
        public int PrixTotal { get; set; }
        public int PrixModif { get; set; }
        public Aspiration Aspiration { get; set; }
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

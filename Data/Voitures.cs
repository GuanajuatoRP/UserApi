using System.ComponentModel.DataAnnotations;
using UserApi.Data.Enum;

namespace UserApi.Data
{
    public class Voitures
    {
        [Key]
        public Guid KeyCar { get; set; }
        public Guid IdCar { get; set; }
        public string IdUser { get; set; }
        public ApiUser User { get; set; }
        public int PowerHp { get; set; }
        public decimal WeightKG { get; set; }
        public DriveTrain DriveTrain { get; set; }
        public Class Class { get; set; }
        public int Pi { get; set; }
        public bool OnRoad { get; set; }
        public decimal Speed { get; set; }
        public decimal Handling { get; set; }
        public decimal Accelerate { get; set; }
        public decimal Launch { get; set; }
        public decimal Braking { get; set; }
        public decimal Offroad { get; set; }
        public int PrixModif { get; set; }
        public int PrixTotal { get; set; }
        public string Imatriculation { get; set; }
    }
}

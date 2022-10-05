using UserApi.Data;
using UserApi.Data.Enum;

namespace UserApi.Models.Garage
{
    public class CarDTO
    {
        public Guid KeyCar { get; set; }
        public string IdUser { get; set; }
        public string? Username { get; set; }
        public Guid IdCar { get; set; }
        public int CarOrdinal { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public string Rarity { get; set; }
        public string WikiLink { get; set; }
        public string? PictureLink { get; set; }


        public decimal OriginalPowerHp { get; set; }
        public decimal OriginalWeightKg { get; set; }
        public string OriginalDriveTrain { get; set; }
        public string OriginalClass { get; set; }
        public int OriginalPi { get; set; }
        public bool OriginalOnRoad { get; set; }
        public decimal OriginalSpeed { get; set; }
        public decimal OriginalHandling { get; set; }
        public decimal OriginalAccelerate { get; set; }
        public decimal OriginalLaunch { get; set; }
        public decimal OriginalBraking { get; set; }
        public decimal OriginalOffroad { get; set; }



        public decimal EditPowerHp { get; set; }
        public decimal EditWeightKg { get; set; }
        public string EditDriveTrain { get; set; }
        public string EditClass { get; set; }
        public int EditPi { get; set; }
        public bool EditOnRoad { get; set; }
        public decimal EditSpeed { get; set; }
        public decimal EditHandling { get; set; }
        public decimal EditAccelerate { get; set; }
        public decimal EditLaunch { get; set; }
        public decimal EditBraking { get; set; }
        public decimal EditOffroad { get; set; }
        public string? Imatriculation { get; set; }
        public int? TotalPrice { get; set; }
        public int? EditPrice { get; set; }
    }
}

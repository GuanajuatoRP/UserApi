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
        public int CarId { get; set; }
        public int CarOrdinal { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Transmission { get; set; }
        public int GearBox { get; set; }
        public string Type { get; set; }
        public string Rarity { get; set; }
        public string WikiLink { get; set; }
        public string? PictureLink { get; set; }
        public string EngineConfiguration { get; set; }


        public decimal OriginalPowerBhp { get; set; }
        public decimal OriginalPowerKw { get; set; }
        public decimal OriginalTorqueLbft { get; set; }
        public decimal OriginalTorqueNm { get; set; }
        public decimal OriginalWeightLbs { get; set; }
        public decimal OriginalWeightKg { get; set; }
        public decimal OriginalEngineDisplacement { get; set; }
        public int OriginalNbCylindre { get; set; }
        public string OriginalAspiration { get; set; }
        public string OriginalEnginePosition { get; set; }
        public decimal OriginalSpeed { get; set; }
        public decimal OriginalHandling { get; set; }
        public decimal OriginalAccelerate { get; set; }
        public decimal OriginalLaunch { get; set; }
        public decimal OriginalBraking { get; set; }
        public decimal OriginalOffroad { get; set; }
        public int OriginalPi { get; set; }
        public int OriginalPrice { get; set; }
        public string OriginalClass { get; set; }



        public decimal? PowerBhp { get; set; }
        public decimal? PowerKw { get; set; }
        public decimal? TorqueLbft { get; set; }
        public decimal? TorqueNm { get; set; }
        public decimal? WeightLbs { get; set; }
        public decimal? WeightKg { get; set; }
        public decimal? EngineDisplacement { get; set; }
        public int? NbCylindre { get; set; }
        public string? Aspiration { get; set; }
        public string? EnginePosition { get; set; }
        public decimal? Speed { get; set; }
        public decimal? Handling { get; set; }
        public decimal? Accelerate { get; set; }
        public decimal? Launch { get; set; }
        public decimal? Braking { get; set; }
        public decimal? Offroad { get; set; }
        public int? Pi { get; set; }
        public string? Class { get; set; }
        public string? Imatriculation { get; set; }
        public int? TotalPrice { get; set; }
        public int? EditPrice { get; set; }
    }
}

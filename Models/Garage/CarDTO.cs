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


        public decimal Original_PowerBhp { get; set; }
        public decimal Original_PowerKw { get; set; }
        public decimal Original_TorqueLbft { get; set; }
        public decimal Original_TorqueNm { get; set; }
        public decimal Original_WeightLbs { get; set; }
        public decimal Original_WeightKg { get; set; }
        public decimal Original_EngineDisplacement { get; set; }
        public int Original_NbCylindre { get; set; }
        public string Original_Aspiration { get; set; }
        public string Original_EnginePosition { get; set; }
        public decimal Original_Speed { get; set; }
        public decimal Original_Handling { get; set; }
        public decimal Original_Accelerate { get; set; }
        public decimal Original_Launch { get; set; }
        public decimal Original_Braking { get; set; }
        public decimal Original_Offroad { get; set; }
        public int Original_Pi { get; set; }
        public int OriginalPrice { get; set; }
        public string Original_Class { get; set; }



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

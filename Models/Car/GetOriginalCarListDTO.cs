namespace UserApi.Models.Car
{
    public class GetOriginalCarListDTO
    {
        public Guid IdCar { get; set; }
        public int CarId { get; set; }
        public int CarOrdinal { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public decimal PowerBhp { get; set; }
        public decimal PowerKw { get; set; }
        public decimal TorqueLbft { get; set; }
        public decimal TorqueNm { get; set; }
        public decimal WeightLbs { get; set; }
        public decimal WeightKg { get; set; }
        public decimal EngineDisplacement { get; set; }
        public int NbCylindre { get; set; }
        public string Aspiration { get; set; } = null!;
        public string EngineConfiguration { get; set; } = null!;
        public string EnginePosition { get; set; } = null!;
        public string Transmission { get; set; }
        public int GearBox { get; set; }
        public decimal Speed { get; set; }
        public decimal Handling { get; set; }
        public decimal Accelerate { get; set; }
        public decimal Launch { get; set; }
        public decimal Braking { get; set; }
        public decimal Offroad { get; set; }
        public int Pi { get; set; }
        public string Class { get; set; } = null!;
        public string RequiredDlc { get; set; } = null!;
        public string Aviability { get; set; } = null!;
        public int Price { get; set; }
        public string Type { get; set; } = null!;
        public string Rarity { get; set; } = null!;
        public string WikiLink { get; set; } = null!;
        public string? PictureLink { get; set; }
    }
}

namespace UserApi.Models.Car
{
    public class OriginalCarDTO
    {
        public Guid IdCar { get; set; }
        public int CarOrdinal { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int PowerHp { get; set; }
        public decimal WeightKg { get; set; }
        public string DriveTrain { get; set; }
        public string Class { get; set; }
        public int Pi { get; set; }
        public bool OnRoad { get; set; }
        public decimal Speed { get; set; }
        public decimal Handling { get; set; }
        public decimal Accelerate { get; set; }
        public decimal Launch { get; set; }
        public decimal Braking { get; set; }
        public decimal Offroad { get; set; }
        public bool RequiredDlc { get; set; }
        public string Aviability { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public string Rarity { get; set; }
        public string WikiLink { get; set; }
        public string PictureLink { get; set; }
    }
}

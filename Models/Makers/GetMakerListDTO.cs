namespace UserApi.Models.Makers
{
    public class GetMakerListDTO
    {
        public Guid IdMaker { get; set; }
        public string Name { get; set; } = null!;
        public string? Origin { get; set; }
    }
}

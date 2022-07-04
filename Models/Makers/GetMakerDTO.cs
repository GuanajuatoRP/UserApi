using UserApi.Models.Car;

namespace UserApi.Models.Makers
{
    public class GetMakerDTO
    {
        public Guid IdMaker { get; set; }
        public string Name { get; set; } = null!;
        public string? Origin { get; set; }
        public int? Founded { get; set; }
        public string? Description { get; set; }
        public string? Related { get; set; }
        public string? WikiLink { get; set; }

        public virtual ICollection<GetOriginalCarListDTO> Cars { get; set; }
    }
}

using UserApi.Models.Car;

namespace UserApi.Models.Makers
{
    public class GetMakerDTO
    {
        public Guid IdMaker { get; set; }
        public string Name { get; set; } = null!;
        public string? Origin { get; set; }

        public virtual ICollection<OriginalCarDTO> Cars { get; set; }
    }
}

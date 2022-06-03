using UserApi.Data;
using UserApi.Models.User;

namespace UserApi.Models.Stages
{
    public class StageDTO
    {
        public Guid StageId { get; set; }
        public string Name { get; set; }

        public List<UserNameDTO> Users { get; set; }
    }
}

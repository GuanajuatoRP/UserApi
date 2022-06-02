using UserApi.Data.Enum;

namespace UserApi.Data
{
    public class Stage
    {
        public Guid StageId { get; set; }
        public StageName Name { get; set; }
        public PermisName PermisRequis { get; set; }
        public StageName StageRequis { get; set; }
        public int NbSessionsRequis { get; set; }

        public ICollection<ApiUser> Users { get; set; }
    }
}

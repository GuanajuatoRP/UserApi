using UserApi.Data.Enum;

namespace UserApi.Models.Stages
{
    public class EditStageDTO
    {
        public Guid StageId { get; set; }
        public StageName Name { get; set; }
        public PermisName PermisRequis { get; set; }
        public StageName StageRequis { get; set; }
        public int NbSessionsRequis { get; set; }
    }
}

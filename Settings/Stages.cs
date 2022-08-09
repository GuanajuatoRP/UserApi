using UserApi.Data;
using UserApi.Data.Enum;

namespace UserApi.Settings
{
    public class StageInit
    {
        public static List<Stage> GetAllStages() => new()
        {
            new Stage()
            {
                Name = StageName.NA,
                PermisRequis = PermisName.NA,
                StageRequis = StageName.NA,
                NbSessionsRequis = 0
            },
            new Stage()
            {
                Name = StageName.B,
                PermisRequis = PermisName.Definitif,
                StageRequis = StageName.NA,
                NbSessionsRequis = 15
            },
            new Stage()
            {
                Name = StageName.A,
                PermisRequis = PermisName.Definitif,
                StageRequis = StageName.B,
                NbSessionsRequis = 30
            },
            new Stage()
            {
                Name = StageName.S1,
                PermisRequis = PermisName.Definitif,
                StageRequis = StageName.A,
                NbSessionsRequis = 45
            },
            new Stage()
            {
                Name = StageName.S2,
                PermisRequis = PermisName.Definitif,
                StageRequis = StageName.S1,
                NbSessionsRequis = 60
            },
        };
    }
}

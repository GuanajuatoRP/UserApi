using UserApi.Data;
using UserApi.Models.Stages;

namespace UserApi.Mapper
{
    public static class StageMapper
    {
        public static StageDTO ToModel(this Stage stage)
        {
            return new StageDTO
            {
                StageId = stage.StageId,
                Name = stage.Name.ToString(),
                Users = stage.Users?.Select(u => u.ToModel()).ToList(),
            };
        }
        public static StageDTO ToModelWithoutUser(this Stage stage)
        {
            return new StageDTO
            {
                StageId = stage.StageId,
                Name = stage.Name.ToString(),
            };
        }
        public static ListStagesDTO ToModelList(this Stage stage)
        {
            return new ListStagesDTO
            {
                StageId = stage.StageId,
                Name = stage.Name.ToString(),
                PermisRequis = stage.PermisRequis.ToString(),
                StageRequis = stage.StageRequis.ToString(),
                NbSessionsRequis = stage.NbSessionsRequis
            };
        }
    }
}

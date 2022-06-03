namespace UserApi.Models.Stages
{
    public class ListStagesDTO
    {
        public Guid StageId { get; set; }
        public string Name { get; set; }
        public string PermisRequis { get; set; }
        public string StageRequis { get; set; }
        public int NbSessionsRequis { get; set; }
    }
}

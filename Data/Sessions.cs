using UserApi.Data.Enum;

namespace UserApi.Data
{
    public class Sessions
    {
        public Guid SessionId { get; set; }
        public SessionType Type { get; set; }
        public string Debut { get; set; }
        public string Fin { get; set; }
        public int NbParticipant { get; set; }
        public int SessionNumber { get; set; }
        public ICollection<ApiUser> Users { get; set; }
    }
}

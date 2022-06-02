using UserApi.Data.Enum;

namespace UserApi.Data
{
    public class Sessions
    {
        public Guid SessionId { get; set; }
        public SessionType Type { get; set; }
        public DateTime Debut { get; set; }
        public DateTime Fin { get; set; }
        public int NbParticipant { get; set; }
        public ICollection<ApiUser> Users { get; set; }
    }
}

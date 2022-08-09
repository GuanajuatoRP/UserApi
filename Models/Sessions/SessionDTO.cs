using UserApi.Models.User;

namespace UserApi.Models.Sessions
{
    public class SessionDTO
    {
        public Guid SessionId { get; set; }
        public string Type { get; set; }
        public string Debut { get; set; }
        public string Fin { get; set; }
        public int NbParticipant { get; set; }
        public int SessionNumber { get; set; }
        public List<UserNameDTO> Users{ get; set; }
    }
}

using UserApi.Models.User;

namespace UserApi.Models.Sessions
{
    public class SessionsDTO
    {
        public Guid SessionId { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public int SessionNumber { get; set; }
        public int NbParticipant { get; set; }
        public List<UserNameDTO> Users { get; set; }
    }
}

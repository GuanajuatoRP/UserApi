namespace UserApi.Models.Sessions
{
    public class SessionsDTO
    {
        public Guid SessionId { get; set; }
        public string Type { get; set; }
        public string Debut { get; set; }
        public string Fin { get; set; }
        public int NbParticipant { get; set; }
    }
}

namespace UserApi.Models.Sessions
{
    public class SessionsDTO
    {
        public Guid SessionId { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public int SessionNumber { get; set; }
        public int NbParticipant { get; set; }
    }
}

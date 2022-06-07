using UserApi.Data;
using UserApi.Models.Sessions;

namespace UserApi.Mapper
{
    public static class SessionsMapper
    {
        public static Sessions CreateClass(this CreateSessionDTO dto)
        {
            return new Sessions
            {
                Type = dto.Type,
                Debut = dto.Debut.ToString("dd/MM/yyyy HH:mm"),
                Fin = dto.Debut.AddHours(1).ToString("dd/MM/yyyy HH:mm"),
                NbParticipant = dto.NbParticipant
            };
        }
        
        public static SessionDTO ToModel(this Sessions session)
        {
            return new SessionDTO
            {
                SessionId = session.SessionId,
                Type = session.Type.ToString(),
                Debut = session.Debut,
                Fin = session.Fin,
                NbParticipant = session.NbParticipant,
                Users = session.Users?.Select(u => u.ToModel()).ToList()
            };
        }

        public static SessionsDTO ToModelList(this Sessions session)
        {
            return new SessionsDTO
            {
                SessionId = session.SessionId,
                Type = session.Type.ToString(),
                Debut = session.Debut,
                Fin = session.Fin,
                NbParticipant = session.NbParticipant
            };
        }
    }
}

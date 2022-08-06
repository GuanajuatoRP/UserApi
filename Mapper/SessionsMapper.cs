using System.Globalization;
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
                Debut = DateTime.UtcNow.AddHours(2).ToString(),
                Fin = DateTime.UtcNow.AddHours(3).ToString(),
                NbParticipant = 0
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
                SessionNumber = session.SessionNumber,
                Users = session.Users?.Select(u => u.ToModel()).ToList()
            };
        }

        public static SessionsDTO ToModelList(this Sessions session)
        {
            return new SessionsDTO
            {
                SessionId = session.SessionId,
                Type = session.Type.ToString(),
                StartDate = session.Debut,
                EndDate = session.Fin,
                NbParticipant = session.NbParticipant,
                SessionNumber = session.SessionNumber,
                Users = session.Users?.Select(u => u.ToModel()).ToList()
            };
        }
    }
}

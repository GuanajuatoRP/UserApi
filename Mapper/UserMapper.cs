using UserApi.Data;
using UserApi.Models.Car;
using UserApi.Models.User;

namespace UserApi.Mapper
{
    public static class UserMapper
    {
        public static UserNameDTO ToModel(this ApiUser user)
        {
            return new UserNameDTO
            {
                UserId = user.Id,
                Username = $"{user.Prenom} {user.Nom}"
            };
        }
        public static UserDTO ToUserModel(this ApiUser user, IEnumerable<OriginalCarDTO> listOriginalCar)
        {
            return new UserDTO
            {
                Id = user.Id,
                Prenom = user.Prenom,
                Nom = user.Nom,
                Username = user.UserName,
                DiscordId = user.Email,
                Sexe = user.Sexe,
                CreatedAt = user.CreatedAt.ToString("dd/MM/yyyy"),
                Argent = user.Argent,
                Permis = user.Permis.ToString(),
                Stage = user.Stage.ToString(),
                Points = user.Points,
                NbSessionsPermis = user.NbSessionsPermis,
                NbSessionsPolice = user.NbSessionsPolice,
                NbSessions = user.NbSessions,
                Voitures = user.Voitures.Select(v => v.ToModel(listOriginalCar.FirstOrDefault(c => c.IdCar == v.IdCar))).ToList()
            };
        }

        public static UserDTO ToUserDTO(this ApiUser user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Prenom = user.Prenom,
                Nom = user.Nom,
                Username = user.UserName,
                DiscordId = user.Email,
                Sexe = user.Sexe,
                CreatedAt = user.CreatedAt.ToString("dd/MM/yyyy"),
                Argent = user.Argent,
                Permis = user.Permis.ToString(),
                Stage = user.Stage.ToString(),
                Points = user.Points,
                NbSessionsPermis = user.NbSessionsPermis,
                NbSessionsPolice = user.NbSessionsPolice,
                NbSessions = user.NbSessions,
            };
        }
        public static List<UserDTO> ToUserDTOList(this List<ApiUser> user)
        {
            return user.Select(u => u.ToUserDTO()).ToList();
        }
    }
}

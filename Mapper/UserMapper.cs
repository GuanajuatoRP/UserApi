using UserApi.Data;
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
    }
}

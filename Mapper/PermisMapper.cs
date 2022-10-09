using UserApi.Data;
using UserApi.Models.Permis;

namespace UserApi.Mapper
{
    public static class PermisMapper
    {
        public static PermisDTO ToPermisDto(this ApiUser user)
        {
            return new PermisDTO
            {
                Username = user.UserName,
                Permis = user.Permis.ToString(),
                Points = user.Points,
                NbSessionsPermis = user.NbSessionsPermis,
                Stage = user.Stage.ToString(),
            };
        }
    }
}

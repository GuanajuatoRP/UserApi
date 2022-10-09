
using UserApi.Data;

namespace UserApi.Models.Permis
{
    public class PermisDTO
    {
        public string Username { get; set; }
        public string Permis { get; set; }
        public string Stage { get; set; }
        public int Points { get; set; }

        public int NbSessionsPermis { get; set; }

    }
}

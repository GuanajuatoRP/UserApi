using Microsoft.AspNetCore.Identity;
using UserApi.Data.Enum;

namespace UserApi.Data
{
    public class ApiUser : IdentityUser
    {
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string? Sexe { get; set; }
        public DateTime CreatedAt { get; set; }


        public int Argent { get; set; }
        public PermisName Permis { get; set; }
        public StageName Stage { get; set; }
        public int Points { get; set; }
        public int NbSessionsPermis { get; set; }
        public int NbSessionsPolice { get; set; }
        public int NbSessions { get; set; }
        public ICollection<Sessions> Sessions { get; set; }
        public ICollection<Voitures> Voitures { get; set; }
    }
}

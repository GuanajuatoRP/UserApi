using Microsoft.AspNetCore.Identity;

namespace UserApi.Data
{
    public class ApiUser : IdentityUser
    {
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Sexe { get; set; }
        public DateTime CreatedAt { get; set; }


        public int Argent { get; set; }
        public string Permis { get; set; }
        public Guid IdStage { get; set; }
        //public virtual Stage Stage { get; set; }
        public int Points { get; set; }
        public int NbSessionsPermis { get; set; }
        public int NbSessionsPolice { get; set; }
        public int NbSessions { get; set; }
        //public ICollection<Sessions> Sessions { get; set; }
        //public ICollection<Voitures> Voitures { get; set; }
    }
}

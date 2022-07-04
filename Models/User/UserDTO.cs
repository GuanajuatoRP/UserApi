using UserApi.Models.Garage;
using UserApi.Models.Stages;

namespace UserApi.Models.User
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Username { get; set; }
        public string DiscordId { get; set; }
        public string Sexe { get; set; }
        public string CreatedAt { get; set; }
        public int Argent { get; set; }
        public string Permis { get; set; }
        public StageDTO Stage { get; set; }
        public int Points { get; set; }
        public int NbSessionsPermis { get; set; }
        public int NbSessionsPolice { get; set; }
        public int NbSessions { get; set; }
        public ICollection<CarDTO> Voitures { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.Permis
{
    public class RetraitPermisDTO
    {
        [Required]
        public int NbSessions { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using UserApi.Data.Enum;

namespace UserApi.Models.Permis
{
    public class SetPermisDTO
    {
        [Required]
        public PermisName Permis { get; set; }
        [Required]
        public int Points { get; set; }
        [Required]
        public int NbSessionsPermis { get; set; }
    }
}

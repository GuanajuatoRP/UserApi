using System.ComponentModel.DataAnnotations;
using UserApi.Data.Enum;

namespace UserApi.Models.Stages
{
    public class CreateStageDTO
    {
        [Required]
        public StageName Name { get; set; }
        [Required]
        public PermisName PermisRequis { get; set; }
        [Required]
        public StageName StageRequis { get; set; }
        [Required]
        public int NbSessionsRequis { get; set; }
    }
}

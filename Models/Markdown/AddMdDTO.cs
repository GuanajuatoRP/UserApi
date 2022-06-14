using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.Markdown
{
    public class AddMdDTO
    {
        [Required]
        public string CatName { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string RawText { get; set; }
        [Required]
        public string FormatType { get; set; }
    }
}

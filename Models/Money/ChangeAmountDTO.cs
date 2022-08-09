using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.Money
{
    public class ChangeAmountDTO
    {
        [Required]
        public int Value { get; set; }
    }
}

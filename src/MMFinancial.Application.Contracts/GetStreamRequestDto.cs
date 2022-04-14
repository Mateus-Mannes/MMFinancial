using System.ComponentModel.DataAnnotations;
namespace MMFinancial
{
    public class GetStreamRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}

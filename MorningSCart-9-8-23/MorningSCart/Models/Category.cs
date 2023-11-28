using System.ComponentModel.DataAnnotations;
namespace MorningSCart.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MinLength(5,ErrorMessage ="Min 5 char req"),MaxLength(45)]
        [Display(Name ="Category Name")]
        public string CName { get; set; }
        public string slug { get; set; }
    }
}

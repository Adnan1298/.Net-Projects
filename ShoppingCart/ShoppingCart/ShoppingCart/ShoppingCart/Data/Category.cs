using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Data
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(10)]
        public string Slug { get; set; }
    }
}

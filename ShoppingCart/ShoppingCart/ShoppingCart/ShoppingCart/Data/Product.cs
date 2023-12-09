using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Data
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        public string Description { get; set; }
        [Required]
        [StringLength(1000)]
        [Display(Name = "Long Discription")]
        public string LDescription { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [StringLength(50)]
        public string Brand { get; set; }
        //public string Image_Url { get; set; }
        [Required]
        [StringLength(10)]
        public string Slug { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Images { get; set; }
        public string UserId { get; set; }
    }
}

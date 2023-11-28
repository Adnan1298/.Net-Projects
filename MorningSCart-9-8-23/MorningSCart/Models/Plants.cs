using System.ComponentModel.DataAnnotations;
namespace MorningSCart.Models
{
    public class Plants
    {
        [Key]
        public int PlantsId { get; set; }
        [StringLength(50)]
        public string ProductName { get; set; }
        [StringLength(150)]
        public string SDescription { get; set; }
        [StringLength(350)]
        public string LDescription { get; set; }
        [StringLength(50)]
        public string Brand { get; set; }
        [StringLength(50)]
        public string PImage { get; set; }
        public int Price { get; set; }
        public string slug { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

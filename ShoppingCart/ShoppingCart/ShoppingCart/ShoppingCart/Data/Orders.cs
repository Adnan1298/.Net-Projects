using Shopping.Data;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Data
{
    public class Orders 
    {
        [Key]
        public int Order_Id { get; set; }
        public string UsersId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        public Product product { get; set; }
        [Display(Name = "Total Amount")]
        public string TotalAmount { get; set; }

        [Required]
        [Display(Name = "Card Horlder Name")]
        public string Cardholdername { get; set; }

        [Required]
        [Display(Name = "Credit Card Number")]
        public long CreditCard { get; set; }
        [Required]
        public string Address { get; set; }

        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public string Email { get; set; }
        public int Quantity { get; set; }
        public string user_product_id { get; set; }
    }
}

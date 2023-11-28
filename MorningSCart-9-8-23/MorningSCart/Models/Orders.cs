using System.ComponentModel.DataAnnotations;

namespace MorningSCart.Models
{
    public class Orders
    {

        [Key]
        public int Order_Id { get; set; }
        public int UsersId { get; set; }
        public int PlantsId { get; set; }
        public Users Users { get; set; }
       public Plants Plants { get; set; }

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

    }
}

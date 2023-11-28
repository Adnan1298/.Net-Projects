namespace MorningSCart.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public long CreditCardNo { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }     
         public int Qty { get; set; }
        public string   TotalAmount { get; set; }
       


    }
}

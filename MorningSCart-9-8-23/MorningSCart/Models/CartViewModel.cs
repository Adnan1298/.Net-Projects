namespace MorningSCart.Models
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public double GrandTotal { get; set; }
        public int pid { get; set; }
    }
}

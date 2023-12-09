namespace ShoppingCart.Data
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public double GrandTotal { get; set; } 
        public string pid { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ShoppingCart.Data
{
    public class CartItem
    {
        public int ProductsId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string pid { get; set; }
        public double Total
        {
            get { return Quantity * Price; }
        }
        public string Image { get; set; }
        public CartItem()
        {
        }
        public CartItem(Product product)
        {
            ProductsId = product.ProductId;
            pid = product.UserId;
            ProductName = product.Name;
            Price = product.Price;
            Quantity = 1;
            Image = product.Images;
        }
    }
}

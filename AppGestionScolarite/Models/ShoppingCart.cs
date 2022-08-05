namespace AppGestionScolarite.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }   
        public ICollection<CartItem> CartItems { get; set; }
    }
}

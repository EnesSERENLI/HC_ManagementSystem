namespace HC.Presentation.Models
{
    public class CartItem
    {
        public CartItem()
        {
            Quantity = 1; //When you add a product to the cart, the number must be at least 1.
        }
        public Guid ProductId { get; set; }
        public Guid EmployeeId { get; set; }
        public string AppUserId { get; set; }
        public int TableId { get; set; }
        public string ProductName { get; set; }
        public short Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? SubTotal { get => Price * Quantity; }
    }
}

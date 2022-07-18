namespace HC.Presentation.Models
{
    public class Cart
    {
        Dictionary<Guid, CartItem> _myCart = new Dictionary<Guid, CartItem>();

        public List<CartItem> myCart { get => _myCart.Values.ToList(); }

        public void AddItem(CartItem cartItem)
        {
            if (_myCart.ContainsKey(cartItem.ProductId))
            {
                _myCart[cartItem.ProductId].Quantity += cartItem.Quantity;
                return;
            }
            _myCart.Add(cartItem.ProductId, cartItem);
        }
    }
}

using Blazored.LocalStorage;

namespace BlazorEcommerce.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public event Action OnChange;

        public async Task AddToCart(CartItem cartItem)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if(cart is null)
            {
                cart = new List<CartItem>();
            }
            cart.Add(cartItem);
            await _localStorage.SetItemAsync("cart", cart);
        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart is null)
            {
                cart = new List<CartItem>();
            }
            return cart;
        }
        private async Task<List<CartItem>> InitCartList()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            return cart is not null ? cart : new List<CartItem>();
        }
    }
}

﻿using System.Security.Claims;

namespace BlazorEcommerce.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<CartProductResponseDto>>> GetCartProducts(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductResponseDto>>
            {
                Data = new List<CartProductResponseDto>()
            };
            foreach (var item in cartItems)
            {
                var product = await _context.Products
                    .Where(p => p.Id == item.ProductId)
                    .FirstOrDefaultAsync();
                if (product is null)
                {
                    continue;
                }
                var productVariant = await _context.ProductVariants
                    .Where(v => v.ProductId == item.ProductId
                    && v.ProductTypeId == item.ProductTypeId)
                    .Include(v => v.ProductType)
                    .FirstOrDefaultAsync();
                if(productVariant is null)
                {
                    continue;
                }

                var cartProduct = new CartProductResponseDto
                {
                    ProductId = item.ProductId,
                    Title = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    ProductType = productVariant.ProductType.Name,
                    ProductTypeId = productVariant.ProductTypeId,
                    Quantity = item.Quantity
                };

                result.Data.Add(cartProduct);
            }
            return result;
        }

        public async Task<ServiceResponse<List<CartProductResponseDto>>> StoreCartItems(List<CartItem> cartItems)
        {
            cartItems.ForEach(cartItem => cartItem.UserId = GetUserId());
            _context.SaveChanges();
            await _context.SaveChangesAsync();

            return await GetDbCartProducts();
        }

        public async Task<ServiceResponse<int>> GetCartItemsCount()
        {
            var count = (await _context.CartItems.Where(ci => ci.UserId == GetUserId()).ToListAsync()).Count;
            return new ServiceResponse<int> {  Data = count };
        }

        public async Task<ServiceResponse<List<CartProductResponseDto>>> GetDbCartProducts()
        {
            return await GetCartProducts(await _context.CartItems
                .Where(ci => ci.UserId == GetUserId()).ToListAsync());
        }

        public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
        {
            cartItem.UserId = GetUserId();

            var sameItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId &&
                ci.ProductTypeId == cartItem.ProductTypeId && ci.UserId == cartItem.UserId);
            if(sameItem is null)
            {
                _context.CartItems.Add(cartItem);
            }
            else
            {
                sameItem.Quantity += cartItem.Quantity;
            }

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public  async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
        {
            var dbCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId &&
                ci.ProductTypeId == cartItem.ProductTypeId && ci.UserId == GetUserId());
            if(dbCartItem is null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = "Cart item does not exist."
                };
            }

            dbCartItem.Quantity= cartItem.Quantity;
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }
    }
}

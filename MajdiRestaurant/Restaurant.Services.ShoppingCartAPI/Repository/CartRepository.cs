using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Services.ShoppingCartAPI.DbContexts;
using Restaurant.Services.ShoppingCartAPI.Models;
using Restaurant.Services.ShoppingCartAPI.Models.Dtos;

namespace Restaurant.Services.ShoppingCartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public CartRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

       
        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            Cart cart = _mapper.Map<Cart>(cartDto);

            var productInDb = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == cartDto.Details.FirstOrDefault().ProductId);
            if(productInDb == null)
            {
                _db.Products.Add(cart.Details.FirstOrDefault().Product);
                await _db.SaveChangesAsync();
            }

            var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cart.Header.UserId);
            if(cartHeaderFromDb == null)
            {
                _db.CartHeaders.Add(cart.Header);
                await _db.SaveChangesAsync();
                cart.Details.FirstOrDefault().CartHeaderId = cart.Header.CardHeaderId;
                cart.Details.FirstOrDefault().Product = null;
                _db.CartDetails.Add(cart.Details.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    u => u.ProductId == cart.Details.FirstOrDefault().ProductId &&
                    u.CartHeaderId == cartHeaderFromDb.CardHeaderId);

                if(cartDetailsFromDb == null)
                {
                    cart.Details.FirstOrDefault().CartHeaderId = cartHeaderFromDb.CardHeaderId;
                    cart.Details.FirstOrDefault().Product = null;
                    _db.CartDetails.Add(cart.Details.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
                else
                {
                    cart.Details.FirstOrDefault().Count += cartDetailsFromDb.Count;
                    cart.Details.FirstOrDefault().Product = null;
                    _db.CartDetails.Update(cart.Details.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartDto>(cart);

        }

        public async Task<bool> ClearCart(string userId)
        {
            try
            {
                var cartHeaderFromDb = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);

                if (cartHeaderFromDb != null)
                {
                    _db.CartDetails.RemoveRange(_db.CartDetails.Where(u => u.CartHeaderId == cartHeaderFromDb.CardHeaderId));
                    _db.CartHeaders.Remove(cartHeaderFromDb);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                return false;
            }
            
         
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            Cart cart = new Cart()
            {
                Header = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId)
            };
            cart.Details =  _db.CartDetails.Where
                (u => u.CartHeaderId == cart.Header.CardHeaderId).Include(u =>u.Product);

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = await _db.CartDetails.FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailsId);
                int totalCountofCartItems = await _db.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).CountAsync();
                _db.CartDetails.Remove(cartDetails);
                if (totalCountofCartItems == 1)
                {
                    var cartHeaderToRemove = await _db.CartHeaders.FirstOrDefaultAsync(u => u.CardHeaderId == cartDetails.CartHeaderId);
                    _db.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
    
    }
}

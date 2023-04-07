using ProductSeller.Models;

namespace ProductSeller.IServices
{
    public interface ICartService
    {
        public bool CreateCart(Cart b);
        public bool UpdateCart(Cart p);
        public bool DeleteCart(Guid Id);
        public List<Cart> GetAllCart();
        public Cart GetCartById(Guid id);
    }
}

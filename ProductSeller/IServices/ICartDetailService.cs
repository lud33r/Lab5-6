using ProductSeller.Models;

namespace ProductSeller.IServices
{
    public interface ICartDetailService
    {
        public bool CreateCartDetail(CartDetails b);
        public bool UpdateCartDetail(CartDetails p);
        public bool DeleteCartlDetail(Guid Id);
        public List<CartDetails> GetAllCartDetail();
        public CartDetails GetCartDetailById(Guid id);
    }
}

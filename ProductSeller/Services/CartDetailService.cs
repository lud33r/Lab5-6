using ProductSeller.IServices;
using ProductSeller.Models;

namespace ProductSeller.Services
{
    public class CartDetailService : ICartDetailService
    {
        ShopDBContext shopDBContext;
        public CartDetailService()
        {
            shopDBContext = new ShopDBContext();
        }
        public bool CreateCartDetail(CartDetails b)
        {
            try
            {
                shopDBContext.CartDetails.Add(b);
                shopDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteCartlDetail(Guid Id)
        {
            try
            {
                //Find(id) chi dung duoc khi id la khoa chinh
                dynamic product = shopDBContext.CartDetails.Find(Id);
                shopDBContext.CartDetails.Remove(product);
                shopDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<CartDetails> GetAllCartDetail()
        {
            return shopDBContext.CartDetails.ToList();
        }

        public CartDetails GetCartDetailById(Guid id)
        {
            return shopDBContext.CartDetails.FirstOrDefault(p => p.Id == id);
        }

        public bool UpdateCartDetail(CartDetails p)
        {
            try
            {
                //Find(id) chi dung duoc khi id la khoa chinh
                dynamic product = shopDBContext.CartDetails.Find(p.Id);
                product.Quantity = p.Quantity;

                //Co the sua them thuoc tinh
                shopDBContext.CartDetails.Update(product);
                shopDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

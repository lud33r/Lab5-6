using ProductSeller.IServices;
using ProductSeller.Models;

namespace ProductSeller.Services
{
    public class CartService : ICartService
    {
        ShopDBContext shopDBContext;
        public CartService()
        {
            shopDBContext = new ShopDBContext();
        }
        public bool CreateCart(Cart b)
        {
            try
            {
                shopDBContext.Carts.Add(b);
                shopDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteCart(Guid Id)
        {
            try
            {
                //Find(id) chi dung duoc khi id la khoa chinh
                dynamic product = shopDBContext.Carts.Find(Id);
                shopDBContext.Carts.Remove(product);
                shopDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Cart> GetAllCart()
        {
                return shopDBContext.Carts.ToList();
        }

        public Cart GetCartById(Guid id)
        {
            return shopDBContext.Carts.FirstOrDefault(p => p.UserId == id);
        }

        public bool UpdateCart(Cart p)
        {
            try
            {
                //Find(id) chi dung duoc khi id la khoa chinh
                dynamic product = shopDBContext.Carts.Find(p.UserId);
                product.Description = p.Description;
                
                //Co the sua them thuoc tinh
                shopDBContext.Carts.Update(product);
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

using ProductSeller.IServices;
using ProductSeller.Models;
using System.Xml.Linq;

namespace ProductSeller.Services
{
    public class UserService : IUserService
    {
        ShopDBContext shopDBContext;
        public UserService()
        {
            shopDBContext = new ShopDBContext();
        }
        public bool CreateUser(User p)
        {
            try
            {
                shopDBContext.Users.Add(p);
                shopDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteUser(Guid Id)
        {
            try
            {
                //Find(id) chi dung duoc khi id la khoa chinh
                dynamic product = shopDBContext.Users.Find(Id);
                shopDBContext.Users.Remove(product);
                shopDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<User> GetAllUsers()
        {
            return shopDBContext.Users.ToList();
        }

        public User GetUserById(Guid id)
        {
            return shopDBContext.Users.FirstOrDefault(p => p.Id ==id);
        }

        public List<User> GetUserByName(string userName)
        {
            return shopDBContext.Users.Where(p => p.Username.Contains(userName)).ToList();
        }

        public bool UpdateUser(User p)
        {
            try
            {
                //Find(id) chi dung duoc khi id la khoa chinh
                dynamic product = shopDBContext.Users.Find(p.Id);
                product.Username = p.Username;
                product.Password = p.Password;
                product.Status = p.Status;
                //Co the sua them thuoc tinh
                shopDBContext.Users.Update(product);
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

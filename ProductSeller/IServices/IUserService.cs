using ProductSeller.Models;

namespace ProductSeller.IServices
{
    public interface IUserService
    {
        public bool CreateUser(User p);
        public bool UpdateUser(User p);
        public bool DeleteUser(Guid Id);
        public List<User> GetAllUsers();
        public User GetUserById(Guid id);
        public List<User> GetUserByName(string userName);
    }
}

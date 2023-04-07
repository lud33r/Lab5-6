using ProductSeller.Models;
using RoleSeller.IServices;

namespace ProductSeller.Services
{
    public class RoleService : IRoleService
    {
        ShopDBContext shopDBContext;
        public RoleService()
        {
            shopDBContext = new ShopDBContext();
        }
        public bool CreateRole(Role p)
        {
            try
            {
                shopDBContext.Roles.Add(p);
                shopDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteRole(Guid Id)
        {
            try
            {
                //Find(id) chi dung duoc khi id la khoa chinh
                dynamic product = shopDBContext.Roles.Find(Id);
                shopDBContext.Roles.Remove(product);
                shopDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Role> GetAllRoles()
        {
            return shopDBContext.Roles.ToList();
        }

        public Role GetRoleById(Guid id)
        {
            return shopDBContext.Roles.FirstOrDefault(p => p.RoleId == id);
        }

        public List<Role> GetRoleByName(string roleName)
        {
            return shopDBContext.Roles.Where(p => p.RoleName.Contains(roleName)).ToList();

        }

        public bool UpdateRole(Role p)
        {
            try
            {
                //Find(id) chi dung duoc khi id la khoa chinh
                dynamic product = shopDBContext.Roles.Find(p.RoleId);
                product.RoleName = p.RoleName;
                product.Description = p.Description;
                product.Status = p.Status;
                //Co the sua them thuoc tinh
                shopDBContext.Roles.Update(product);
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

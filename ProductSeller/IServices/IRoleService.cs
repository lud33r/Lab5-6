using ProductSeller.Models;


namespace RoleSeller.IServices
{
    public interface IRoleService
    {
        public bool CreateRole(Role p);
        public bool UpdateRole(Role p);
        public bool DeleteRole(Guid Id);
        public List<Role> GetAllRoles();
        public Role GetRoleById(Guid id);
        public List<Role> GetRoleByName(string roleName);
    }
}

using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ProductSeller.Models
{
    public class ShopDBContext:DbContext
    {
        public ShopDBContext() { }
        public ShopDBContext(DbContextOptions options):base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetails> BillDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LEMINHHIEU\SQLEXPRESS;Initial Catalog=ProductSeller;Persist Security Info=True;User ID=lmaohieu;Password=1");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

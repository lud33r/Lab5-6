using ProductSeller.IServices;
using ProductSeller.Models;

namespace ProductSeller.Services
{
    public class ProductService : IProductService
    {
        ShopDBContext shopDBContext;
        public ProductService()
        {
            shopDBContext = new ShopDBContext();
        }
        public bool CreateProduct(Product p)
        {
            try
            {
                shopDBContext.Products.Add(p);
                shopDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProduct(Guid Id)
        {
            try
            {
                //Find(id) chi dung duoc khi id la khoa chinh
                dynamic product = shopDBContext.Products.Find(Id);
                shopDBContext.Products.Remove(product);
                shopDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> GetAllProducts()
        {
            return shopDBContext.Products.ToList(); 
        }

        public Product GetProductById(Guid id)
        {
            return shopDBContext.Products.FirstOrDefault(p => p.Id == id);//p => p.Id == id la predicate duoc call back lai
        }

        public List<Product> GetProductByName(string name)
        {
            return shopDBContext.Products.Where(p => p.Name.Contains(name)).ToList();
        }

        public bool UpdateProduct(Product p)
        {
            try
            {
                //Find(id) chi dung duoc khi id la khoa chinh
                dynamic product = shopDBContext.Products.Find(p.Id);
                product.Name = p.Name;
                product.Price = p.Price;
                product.AvailableQuantity = p.AvailableQuantity;
                product.Status = p.Status;
                product.Supplier = p.Supplier;
                product.Description = p.Description;    
                //Co the sua them thuoc tinh
                shopDBContext.Products.Update(product);
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

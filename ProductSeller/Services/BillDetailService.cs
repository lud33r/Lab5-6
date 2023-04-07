using ProductSeller.IServices;
using ProductSeller.Models;

namespace ProductSeller.Services
{
    public class BillDetailService : IBillDetailService
    {
        ShopDBContext shopDBContext;
        public BillDetailService()
        {
            shopDBContext = new ShopDBContext();    
        }
        public bool CreateBillDetail(BillDetails b)
        {
            try
            {
                shopDBContext.BillDetails.Add(b);
                shopDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteBillDetail(Guid Id)
        {
            try
            {
                //Find(id) chi dung duoc khi id la khoa chinh
                dynamic product = shopDBContext.BillDetails.Find(Id);
                shopDBContext.BillDetails.Remove(product);
                shopDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BillDetails> GetAllBillDetails()
        {
            return shopDBContext.BillDetails.ToList();
        }

        public BillDetails GetBillDetailById(Guid id)
        {
            return shopDBContext.BillDetails.FirstOrDefault(p => p.Id == id);
        }

        public bool UpdateBillDetail(BillDetails p)
        {
            try
            {
                //Find(id) chi dung duoc khi id la khoa chinh
                dynamic product = shopDBContext.BillDetails.Find(p.Id);
                product.Quantity = p.Quantity;
                product.Price = p.Price;

                //Co the sua them thuoc tinh
                shopDBContext.BillDetails.Update(product);
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

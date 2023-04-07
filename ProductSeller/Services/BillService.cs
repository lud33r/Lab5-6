using ProductSeller.IServices;
using ProductSeller.Models;

namespace ProductSeller.Services
{
    public class BillService : IBillService
    {
        ShopDBContext shopDBContext;
        public BillService()
        {
            shopDBContext = new ShopDBContext();
        }
        public bool CreateBill(Bill b)
        {
            try
            {
                shopDBContext.Bills.Add(b);
                shopDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteBill(Guid Id)
        {
            try
            {
                //Find(id) chi dung duoc khi id la khoa chinh
                dynamic product = shopDBContext.Bills.Find(Id);
                shopDBContext.Bills.Remove(product);
                shopDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Bill> GetAllBills()
        {
            return shopDBContext.Bills.ToList();
        }

        public Bill GetBillById(Guid id)
        {
            return shopDBContext.Bills.FirstOrDefault(p => p.Id == id);
        }

        public bool UpdateBill(Bill p)
        {
            try
            {
                //Find(id) chi dung duoc khi id la khoa chinh
                dynamic product = shopDBContext.Bills.Find(p.Id);
                product.Status = p.Status;

                //Co the sua them thuoc tinh
                shopDBContext.Bills.Update(product);
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

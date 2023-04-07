using ProductSeller.Models;

namespace ProductSeller.IServices
{
    public interface IBillService
    {
        public bool CreateBill(Bill b);
        public bool UpdateBill(Bill p);
        public bool DeleteBill(Guid Id);
        public List<Bill> GetAllBills();
        public Bill GetBillById(Guid id);
        
    }
}

using ProductSeller.Models;

namespace ProductSeller.IServices
{
    public interface IBillDetailService
    {
        public bool CreateBillDetail(BillDetails b);
        public bool UpdateBillDetail(BillDetails p);
        public bool DeleteBillDetail(Guid Id);
        public List<BillDetails> GetAllBillDetails();
        public BillDetails GetBillDetailById(Guid id);
    }
}

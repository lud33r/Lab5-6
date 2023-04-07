namespace ProductSeller.Models
{
    public class CartDetails
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid IdSP { get; set; }
        public int Quantity { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }

    }
}

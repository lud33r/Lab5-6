namespace ProductSeller.Models
{
    public class Cart
    {
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<CartDetails> CartDetails { get; set; }
        public virtual User User { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductSeller.Models;

namespace ProductSeller.Configuration
{
    public class BillDetailsConfig : IEntityTypeConfiguration<BillDetails>
    {
        public void Configure(EntityTypeBuilder<BillDetails> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Quantity).IsRequired().HasColumnType("int");

            //set khoá ngoại
            builder.HasOne(p => p.Bill).WithMany(p => p.BillDetails).HasForeignKey(p => p.IdHD).HasConstraintName("FK_HD");
            builder.HasOne(p => p.Product).WithMany(p => p.BillDetails).HasForeignKey(p => p.IdSP).HasConstraintName("FK_SP");

        }
    }
}

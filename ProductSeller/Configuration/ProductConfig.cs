using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductSeller.Models;


namespace ProductSeller.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            /*builder.ToTable("SanPham");*///Đặt tên bảng
            builder.HasKey(p => new { p.Id });//Thiết lập khoá chính
            //Thiết lập các thuộc tính
            builder.Property(p => p.Name).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Supplier).IsUnicode(true).IsFixedLength().HasMaxLength(100);// = nvarchar
        }
    }
}

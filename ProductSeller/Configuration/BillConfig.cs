using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductSeller.Models;


namespace ProductSeller.Configuration
{
    public class BillConfig : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            //builder.ToTable("HoaDon");//Đặt tên bảng
            builder.HasKey(p => new {p.Id});//Thiết lập khoá chính
            //Thiết lập các thuộc tính
            builder.Property(p => p.Status).HasColumnType("int").
                IsRequired();//int not null
            builder.HasOne(p => p.User).WithMany(p => p.Bills).HasForeignKey(p =>p.UserId);
        }
    }
}

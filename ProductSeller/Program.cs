using ProductSeller.IServices;
using ProductSeller.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductService, ProductService>();
/*
    AddSingleTon: Tao ra 1 doi tuong service ton tai cho den khi vong doi cua ung dung ket thuc. Service nay se duoc dung chung cho cac request. Loai dang ki nay phu hop voi cac service mang tinh toan cuc va khong thay doi
    AddScoped: Moi request cu the se tao ra 1 doi tuong service, doi tuong nay duoc giu nguyen trong qua trinh su ly request phu hop cho cac service ma phuc vu cho 1 loai request cu the
    AddTransient: Moi request se nhan 1 service cu the khi co yeu cau. Moi service se duoc tao moi tai thoi diem co yeu cau. Phu hop cho cac service co nhieu trang thai, nhieu yeu cau http va mang tinh linh dong hon
 */

//dictionary<string, timespan> sessiontimes = 
//new dictionary<string, timespan>();

//// set timespan cho từng session
//sessiontimes.add("cart", new timespan(0, 0, 10)); // 30 phút cho session1
//sessiontimes.add("test", new timespan(0, 0, 10)); // 1 giờ cho session2
//sessiontimes.add("mitom2trung", new timespan(0, 0, 20)); // 45 phút cho session3

//// đăng ký session vào dịch vụ
//builder.services.addsession(options =>
//{
//    // thiết lập thời gian timeout cho session tương ứng với từng key trong dictionary
//    foreach (var key in sessiontimes.keys)
//    {
//        timespan duration;
//        if (sessiontimes.trygetvalue(key, out duration))
//        {
//            options.idletimeout = duration;
//        }
//    }
//});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1);
});//them cai nay de su dung duoc session voi timeout = 10 giay



//tat ca dich vu dang ky phai truoc dong nay    
var app = builder.Build();//thuc hien tat ca cac service duoc cai dat

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();//them cai nay de su dung duoc session
app.UseRouting();
app.UseStatusCodePagesWithReExecute("/Home/Index");//Middleware tự động redirect người dùng đến thẳng /home/index khi gặp bất kì lỗi nào về http status code
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

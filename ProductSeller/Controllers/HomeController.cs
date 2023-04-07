using Microsoft.AspNetCore.Mvc;
using ProductSeller.IServices;
using ProductSeller.Models;
using ProductSeller.Services;
using System.Diagnostics;

namespace ProductSeller.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            productService = new ProductService();//dependency injection
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var sessionData = HttpContext.Session.GetString("goilagi");

            ViewData["data"] = sessionData;
            return View();
        }
        public IActionResult Test()
        {

            // Đọc dữ liệu ra từ Session
            var sessionData = HttpContext.Session.GetString("mitom2trung");
            if (sessionData == null)
                ViewData["data"] = "Session không tồn tại";
            else ViewData["data"] = sessionData;
            var sessionData2 = HttpContext.Session.GetString("Test");
            if (sessionData2 == null)
                ViewData["data2"] = "Session không tồn tại";
            else ViewData["data2"] = sessionData2;
            return View();
        }

        public IActionResult Redirect()
        {
            return RedirectToAction("Test");   //Chuyen huong ve action test
        }
        //truyen du lieu action qua view


        public ActionResult ShowAllProduct()
        {
            List<Product> products = productService.GetAllProducts();
            return View(products);//truyen truc tiep 1 object model duy nhat sang view
        }

        public IActionResult Create()
        {

            return View();

        }
        [HttpPost]
        // Các ảnh không nằm trong thư mục root khi chạy sẽ không hiển thị
        // ra với các phương thức cơ bản => để hienr thị được ta cần phải
        // thực hiện cách sau: Lấy đường dẫn ảnh => copy ảnh đó vào wwwroot
        // sau đó thực hiện hiển thị như bình thường
        public IActionResult Create(Product p, IFormFile imageFile)
        {
            //trong trường hop chúng ta thực hiện với thuộc tính description. Thuộc tính này đang là string => không thể thao tác trực tiếp với các file => truyền thêm 1 tham số vào action này 
            //truyền thêm 1 tham số imageFile kiểu ÌormFile
            //Bước 1: kiểm tra đường dẫn tới ảnh đc lấy từ form
            if (imageFile != null && imageFile.Length > 0)
            {
                //thực hiện trỏ tới thư mục root để thực hiện copy
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageFile.FileName);
                //kết quả: aa/wwwroot/images/fsdfas.jpg
                //directoty trỏ thẳng vào dự án đang làm
                //path.combine tổng họp các đường dẫn
                var stream = new FileStream(path, FileMode.Create);
                //vì chúng ta thực hiện copy => tạo mới => create
                imageFile.CopyTo(stream);//copy ảnh chọn ở form vào wwwroot/images
                //gán lại giá trị cho thuộc tính description => bước 3
                p.Description = imageFile.FileName;//bước 4
            }
            if (productService.CreateProduct(p))
            {
                return RedirectToAction("ShowAllProduct");
            }
            else return BadRequest();
        }


        public IActionResult GetImage(string imagePath)
        {
            string defaultPath = @"C:\Users\HPS\Pictures\Newfolder\handsome.png";
            byte[] imageBytes;
            try
            {
                imageBytes = System.IO.File.ReadAllBytes(imagePath);
            }
            catch (Exception)
            {
                imageBytes = System.IO.File.ReadAllBytes(defaultPath);
            }
            return File(imageBytes, "image/jpeg");
        }
        public IActionResult Details(Guid id)
        {
            var product = productService.GetProductById(id);
            return View(product);
        }

        public IActionResult Delete(Guid id)
        {

            if (productService.DeleteProduct(id))
            {
                return RedirectToAction("ShowAllProduct");
            }
            else return BadRequest();
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var product = productService.GetProductById(id);
            var products = SessionService.GetObjectFromSession(HttpContext.Session, "History");
            var existingProduct = products.FirstOrDefault(p => p.Id == id);
            if (products.Count == 0)
            {
                products.Add(product);
                SessionService.SetObjectToSession(HttpContext.Session, "History", products);
            }
            else
            {
                if (SessionService.CheckObjectInList(id,products))
                {
                    if (existingProduct!=null)
                    {
                        return View(product);
                    }
                    else 
                    {
                        products.Remove(existingProduct);
                    }
                }
                else
                {
                    products.Add(product);
                    SessionService.SetObjectToSession(HttpContext.Session, "History", products);
                }
            }
            return View(product);
        }


        public IActionResult Edit(Product p)
        {
            if (productService.UpdateProduct(p))
            {
                return RedirectToAction("ShowAllProduct");
            }
            else return BadRequest();
        }



        public IActionResult ViewBag_ViewData()//dung duoc o 1 view
        {
            var product = productService.GetAllProducts();
            //viewbag chua du lieu dang dynamic, khi can su dung ta khong can khoi tao ma gan thang gia tri vao
            //view bag loi nhieu hon do ep kieu nhieu
            ViewBag.Products = product;
            ViewBag.Messages = "Tra lai tam tri toi day";
            //viewdata chua du lieu dang generic, du lieu nay duoc luu duoi dang key-value
            //viewdata truyen du lieu nhanh hon do xu li du lieu dang key-value it phuc tap hon

            ViewData["Products"] = product;
            ViewData["Values"] = "Moew moew moew moew";
            //trong do "Products" la key con product la value
            return View();
        }

        public IActionResult TestSession()
        {

            string message = "Cool text";

            //dua du lieu vao phien lam viec (Session)
            HttpContext.Session.SetString("goilagi", message);

            //dua du lieu ra
            var sessionData = HttpContext.Session.GetString("goilagi");

            ViewData["data"] = sessionData;
            return View();

            /*timeout cua sesion duoc tinh nhu:
             * khi session da ton tai bo dem thoi gian se duoc kich hoat ngau sau khi request cuoi cung duoc thuc thi. Neu sau khoang thoi gian idleTimeout ma ko co request nao duoc thuc thi thi du lieu do se mat. Neu truoc khi thoi gian timeout ket thuc ma co 1 request bat ki duoc thuc thi thi bo dem thoi gian se duoc reset 
             */
        }


        public IActionResult AddToCart(Guid id)
        {
            //B1: Dua vao id lay ra san pham
            var product = productService.GetProductById(id);
            //B2: Lay danh sach san pham ra tu session
            var products = SessionService.GetObjectFromSession(HttpContext.Session, "Cart");
            if (products.Count == 0)
            {
                products.Add(product);// them truc tiep sp vao neu list trong
                SessionService.SetObjectToSession(HttpContext.Session, "Cart", products);
            }
            else
            {
                if (SessionService.CheckObjectInList(id, products))
                {//kiem tra xem list lay ra co chua sp hay chua
                    return Content("Binh thuong chung ta se them so luong nhung vi luoi nen che ma chi dua ra thong bao nay");
                }
                else
                {
                    products.Add(product);// them truc tiep sp vao neu list trong chua chua sp 
                    SessionService.SetObjectToSession(HttpContext.Session, "Cart", products);
                }
            }
            //B3: Kiem tra va them san pham vao gio hang
            return RedirectToAction("ShowCart");
        }

        public IActionResult ShowCart()
        {
            //Lay du lieu tu session de truyen vao view
            var products = SessionService.GetObjectFromSession(HttpContext.Session, "Cart");
            return View(products);
        }

        [HttpPost]
        public IActionResult CallBack(Guid id, string action) 
        {
            if (action == "CallBack")
            {
                var obj = SessionService.GetObjectFromSession(HttpContext.Session, "History").FirstOrDefault(p =>p.Id == id);
                if (productService.UpdateProduct(obj))
                {
                    return RedirectToAction("ShowAllProduct");
                }
                else return BadRequest();
            }
            else 
            {
                var product = productService.GetProductById(id);
                return RedirectToAction("UpdateProduct", product);
            }
        }


        public IActionResult HistoryEdit() 
        {
            var products = SessionService.GetObjectFromSession(HttpContext.Session, "History");
            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
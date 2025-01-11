using Microsoft.AspNetCore.Mvc;
using ThanhThoaiRestaurant.Models;
using System.Linq;
using X.PagedList;
using X.PagedList.Mvc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using System.Text.Json;


namespace ThanhThoaiRestaurant.Controllers
{
    public class ProductController : Controller
    {     
        public int PageSize = 9;
		private readonly QuanLyNhaHangContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductController(QuanLyNhaHangContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
		}

		public  async Task<IActionResult> Index( int productPage =1 )
        {
            return View(
                new ProductListViewModel
                {
                    MonAns = _context.MonAns
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemPerPage = PageSize,
                        CurrentPage = productPage,
                        TotalItems = _context.MonAns.Count()
                    }
                }
                );
        }

        [HttpPost]
        public async Task<IActionResult> Search(string keywords, int productPage = 1)
        {
            var query = string.IsNullOrWhiteSpace(keywords)
                ? _context.MonAns 
                : _context.MonAns.Where(p => p.TenMon.Contains(keywords)); 

            int totalItems = query.Count();

            return View("Index",
                new ProductListViewModel
                {
                    MonAns = query
                        .Skip((productPage - 1) * PageSize) 
                        .Take(PageSize), 
                    PagingInfo = new PagingInfo
                    {
                        ItemPerPage = PageSize,
                        CurrentPage = productPage,
                        TotalItems = totalItems 
                    }
                }
            );
        }


        public IActionResult Details(int id)
		{
			var menuItem = _context.MonAns
                .FirstOrDefault(m => m.MaMon == id);

			if (menuItem == null)
			{
				return NotFound();
			}

			ViewBag.MenuItem = menuItem; // Truyền dữ liệu món ăn vào ViewBag
			return View(menuItem);
		}

        [HttpPost]
        public IActionResult ThemVaoGioHang(int maMon, int soLuongMM)
        {
            var monAn = _context.MonAns.FirstOrDefault(m => m.MaMon == maMon);

            if (monAn == null)
            {
                return Json(new { success = false, message = "Món ăn không tồn tại." });
            }
            var gioHangJson = HttpContext.Session.GetString("GioHang");
            List<ChiTietGh> chiTietGhs;

            if (gioHangJson != null)
            {
                chiTietGhs = JsonSerializer.Deserialize<List<ChiTietGh>>(gioHangJson);
            }
            else
            {
                chiTietGhs = new List<ChiTietGh>();
            }

            var existingItem = chiTietGhs.FirstOrDefault(item => item.MaMon == maMon);

            if (existingItem != null)
            {
                // Nếu món ăn đã tồn tại trong giỏ hàng, cập nhật số lượng
                existingItem.SoLuongMM += soLuongMM;
            }
            else
            {
                chiTietGhs.Add(new ChiTietGh
                {
                    MaMon = maMon,
                    SoLuongMM = soLuongMM,
                    TenMon = monAn.TenMon, // Lấy tên món ăn từ CSDL
                    HinhAnh = monAn.HinhAnh, // Lấy hình ảnh từ CSDL
                    GiaBan = (float)monAn.GiaBan,
                    ThanhTien = (float)(monAn.GiaBan * soLuongMM),
                    SoLuong = monAn.SoLuong,
                    MaMonNavigation = monAn
                });
            }

            // Lưu giỏ hàng vào Session
            HttpContext.Session.SetString("GioHang", JsonSerializer.Serialize(chiTietGhs));

            return Json(new { success = true, message = "Món đã được thêm vào giỏ hàng." });
        }

        public class PriceRange
        {
            public int Min { get; set; }
            public int Max { get; set; }
        }

        public class FilteredProductsViewModel
        {
            public List<MonAn> FilteredProducts { get; set; }
            public PagingInfo PagingInfo { get; set; }
        }

        [HttpPost]
        public IActionResult GetFilterProducts([FromBody] FilterData filter)
        {
            // Sử dụng Include để kết nối thông tin của OCung
            var filterProducts = _context.MonAns
                .Include (m => m.CPU)
                .ToList();

            

            if (filter.Category != null && filter.Category.Count > 0 && !filter.Category.Contains("all"))
            {
                filterProducts = filterProducts.Where(p => filter.Category.Contains(p.TenNhom)).ToList();
            }

            if (filter.Hardware != null && filter.Hardware.Count > 0 && !filter.Hardware.Contains("all"))
            {
                filterProducts = filterProducts
                                        .Where(p => p.CPU != null
                                                    && !string.IsNullOrEmpty(p.CPU.TenLoaiCPU)
                                                    && filter.Hardware.Any(hw => hw.Trim().ToLower() == p.CPU.TenLoaiCPU.Trim().ToLower()))
        .ToList();
            }
            // Sắp xếp dựa trên SortOption
            if (!string.IsNullOrEmpty(filter.SortOption))
            {
                switch (filter.SortOption)
                {
                    case "1": // Giá từ thấp đến cao
                        filterProducts = filterProducts.OrderBy(p => p.GiaBan).ToList();
                        break;
                    case "2": // Giá từ cao xuống thấp
                        filterProducts = filterProducts.OrderByDescending(p => p.GiaBan).ToList();
                        break;
                    default: // Mặc định hoặc Bán chạy
                        filterProducts = filterProducts.OrderByDescending(p => p.SoLuongDaBan).ToList();
                        break;
                }
            }
            return PartialView("_ReturnProducts",filterProducts);
        }

		[HttpPost]
		public async Task<IActionResult> SubmitReview(int maMon, string content, int stars,
	IFormFile image1, IFormFile image2, IFormFile image3, IFormFile image4, IFormFile image5,
	IFormFile video)
		{
			// Kiểm tra xem MaMon có tồn tại trong CSDL không
			var product = _context.MonAns.FirstOrDefault(p => p.MaMon == maMon);
			if (product == null)
			{
				// Trả về một lỗi nếu không tìm thấy sản phẩm với MaMon đã cung cấp
				return NotFound();
			}

			// Lấy thư mục lưu trữ ảnh và video
			string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

			// Tạo thư mục lưu trữ nếu chưa tồn tại
			if (!Directory.Exists(uploadDir))
			{
				Directory.CreateDirectory(uploadDir);
			}

			// Lưu hình ảnh vào thư mục lưu trữ và gán tên file cho các hình ảnh
			string image1FileName = await SaveImage(image1, uploadDir);
			string image2FileName = await SaveImage(image2, uploadDir);
			string image3FileName = await SaveImage(image3, uploadDir);
			string image4FileName = await SaveImage(image4, uploadDir);
			string image5FileName = await SaveImage(image5, uploadDir);

			// Lưu video vào thư mục lưu trữ nếu có
			string videoFileName = await SaveVideo(video, uploadDir);

			// Tạo một đối tượng Đánh giá mới
			var danhGia = new DanhGia
			{
				MaDanhGia = GenerateRandomMaDanhGia(), // Tạo số ngẫu nhiên có 4 chữ số
				MaMon = maMon, // Lấy MaMon từ sản phẩm
				TenDangNhap = HttpContext.Session.GetString("TenDangNhap"), // Lấy Tên đăng nhập từ phiên đăng nhập hiện tại
				NoiDung = content, // Lấy nội dung từ form
				NgayDG = DateTime.Now, // Lấy ngày hiện tại
				Diem = stars, // Lấy điểm từ form
				HinhAnh1 = GetFileNameOrDefault(image1FileName),
				HinhAnh2 = GetFileNameOrDefault(image2FileName),
				HinhAnh3 = GetFileNameOrDefault(image3FileName),
				HinhAnh4 = GetFileNameOrDefault(image4FileName),
				HinhAnh5 = GetFileNameOrDefault(image5FileName),
				Video = GetFileNameOrDefault(videoFileName)
			};

			// Thêm đánh giá mới vào trong CSDL
			_context.DanhGias.Add(danhGia);
			_context.SaveChanges();

			// Sau khi thêm đánh giá, bạn có thể chuyển hướng người dùng đến trang chi tiết sản phẩm hoặc thực hiện hành động khác
			return RedirectToAction("Details", "Product", new { id = maMon });
		}

		public IActionResult DetailsReview(int id)
		{
			var menuItem = _context.DanhGias
				.FirstOrDefault(m => m.MaDanhGia == id);

			if (menuItem == null)
			{
				return NotFound();
			}

			ViewBag.MenuItem = menuItem; // Truyền dữ liệu món ăn vào ViewBag
			return View(menuItem);
		}

		private async Task<string> SaveImage(IFormFile image, string uploadDir)
		{
			if (image != null && image.Length > 0)
			{
				// Tạo tên file mới
				string imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
				string imagePath = Path.Combine(uploadDir, imageFileName);

				// Lưu tệp vào thư mục lưu trữ
				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					await image.CopyToAsync(stream);
				}

				return imageFileName;
			}

			// Trả về giá trị null nếu không có hình ảnh được tải lên
			return null;
		}

		private async Task<string> SaveVideo(IFormFile video, string uploadDir)
		{
			if (video != null && video.Length > 0)
			{
				// Tạo tên file mới
				string videoFileName = Guid.NewGuid().ToString() + Path.GetExtension(video.FileName);
				string videoPath = Path.Combine(uploadDir, videoFileName);

				// Lưu tệp vào thư mục lưu trữ
				using (var stream = new FileStream(videoPath, FileMode.Create))
				{
					await video.CopyToAsync(stream);
				}

				return videoFileName;
			}

			// Trả về giá trị null nếu không có video được tải lên
			return null;
		}

		private string GetFileNameOrDefault(string fileName)
		{
			return fileName ?? null;
		}

		// Phương thức này sẽ tạo số ngẫu nhiên có 4 chữ số và chưa tồn tại trong CSDL
		private int GenerateRandomMaDanhGia()
		{
			Random random = new Random();
			int randomNumber;
			do
			{
				randomNumber = random.Next(1000, 9999); // Tạo số ngẫu nhiên có 4 chữ số
			} while (_context.DanhGias.Any(dg => dg.MaDanhGia == randomNumber)); // Kiểm tra xem số này đã tồn tại trong CSDL chưa
			return randomNumber;
		}
	}
}


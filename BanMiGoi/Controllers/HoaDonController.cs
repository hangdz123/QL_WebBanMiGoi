using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThanhThoaiRestaurant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using System.Text.Json;
using Newtonsoft.Json;
using ThanhThoaiRestaurant.Services;

namespace ThanhThoaiRestaurant.Controllers
{
    public class HoaDonController : Controller
    {

        private readonly QuanLyNhaHangContext _context;

        private readonly IVnPayService _vnPayService;

        private readonly IMailService _mailService;
        //injecting IMailService vào constructor
        private readonly IPayPalService _payPalService;
        public HoaDonController(QuanLyNhaHangContext context, IVnPayService vnPayService, IMailService _MailService, IPayPalService payPalService)
        {
            _context = context;
            _vnPayService = vnPayService;
            _mailService = _MailService;
            _payPalService = payPalService;
            
        }
        public IActionResult Index()
        {
            string tenDangNhap = HttpContext.Session.GetString("TenDangNhap"); // Lấy tên đăng nhập từ session
            if (tenDangNhap == null)
            {
                // Xử lý trường hợp người dùng chưa đăng nhập
                return View("Error");
            }

            var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.TenDangNhap == tenDangNhap);
            if (khachHang == null)
            {
                // Xử lý trường hợp không tìm thấy thông tin khách hàng
                return View("Error");
            }

            var gioHang = HttpContext.Session.GetObject<List<ChiTietGh>>("GioHang");

            

            // Tạo một danh sách tạm thời để lưu thông tin món ăn
            var danhSachMonAn = new List<ChiTietHd>();

            float TongTien = 0;
            float TongGiamGia = 0;

            foreach (var item in gioHang)
            {
                ChiTietHd chiTietHd = new ChiTietHd
                {
                    TenMon = item.TenMon,
                    SoLuongCt = item.SoLuongMM,
                    ThanhTien = item.ThanhTien
                };

                danhSachMonAn.Add(chiTietHd);
                TongTien += (float)chiTietHd.ThanhTien;
            }

            if (TongTien > 1000000)
            {
                TongGiamGia = TongTien/20;
            }

            float TienThanhToan = TongTien - TongGiamGia;
            // Lặp qua danh sách sản phẩm trong giỏ hàng và thêm thông tin món ăn vào danh sách tạm thời
            

            // Tạo một đối tượng HoaDonViewModel và điền thông tin khách hàng vào
            var hoaDonViewModel = new HoaDonViewModel
            {

                TenKh = khachHang.TenKh,
                EmailKh = khachHang.EmailKh,
                Sdtkh = khachHang.Sdtkh,
                DiaChiKh = khachHang.DiaChiKh,
                GioiTinhKh = khachHang.GioiTinhKh,

                ChiTietHoaDon = danhSachMonAn,

                TongTien = TongTien,
                TienGiam = TongGiamGia,
                TienTt = TienThanhToan

                // Điền các thuộc tính khác của khách hàng
            };



         
            return View(hoaDonViewModel);
        }

        [HttpPost]
        public IActionResult XacNhanThanhToan(HoaDonViewModel model)
        {

            string tenDangNhap = HttpContext.Session.GetString("TenDangNhap"); // Lấy tên đăng nhập từ session
            if (tenDangNhap == null)
            {
                // Xử lý trường hợp người dùng chưa đăng nhập
                return View("Error");
            }

            var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.TenDangNhap == tenDangNhap);
            if (khachHang == null)
            {
                // Xử lý trường hợp không tìm thấy thông tin khách hàng
                return View("Error");
            }
            // Lấy thông tin giỏ hàng từ Session
            var gioHang = HttpContext.Session.GetObject<List<ChiTietGh>>("GioHang");

            if (gioHang == null || gioHang.Count == 0)
            {
                // Xử lý trường hợp giỏ hàng rỗng
                // Có thể chuyển người dùng đến trang thông báo lỗi hoặc trang giỏ hàng
                return View("GioHangRong");
            }

            var danhSachMonAn = new List<ChiTietHd>();

            float TongTien = 0;
            float TienGiam = 0;

            // Lặp qua danh sách món ăn trong giỏ hàng để tạo danh sách chi tiết hoá đơn
            foreach (var item in gioHang)
            {
                ChiTietHd chiTietHd = new ChiTietHd
                {
                    MaMon = item.MaMon,
                    TenMon = item.TenMon,
                    SoLuongCt = item.SoLuongMM,
                    ThanhTien = item.ThanhTien,
                    HinhAnhHd = item.HinhAnh
                };

                danhSachMonAn.Add(chiTietHd);
                TongTien += (float)chiTietHd.ThanhTien;
            }



            // Kiểm tra nếu tổng tiền mua hàng vượt qua 1,000,000 thì áp dụng giảm giá 5%
            if (TongTien > 1000000)
            {
                TienGiam = TongTien / 20;
            }

            float TienTt = (TongTien - TienGiam);

            // Lưu hoá đơn vào CSDL
            var hoaDon = new HoaDon
            {
                MaHd = GenerateRandomCustomerCode(),
                NgayHd = DateTime.Now,
                TongTien = TongTien,
                TienGiam = TienGiam,
                TienTt = TienTt,
                MaPhieuGg = 001,
                HinhThucTT = model.HinhThucTT,
                TrangThaiHD = 1
                // Điền các thông tin hoá đơn từ Model khác
            };

            // Lưu hoá đơn vào CSDL
            _context.HoaDons.Add(hoaDon);
            _context.SaveChanges();

            // Lấy mã hoá đơn sau khi đã lưu vào CSDL
            int maHoaDon = hoaDon.MaHd;

            // Lưu chi tiết hoá đơn vào CSDL
            foreach (var chiTiet in danhSachMonAn)
            {
                var chiTietHoaDon = new ChiTietHd
                {
                    MaHd = maHoaDon,
                    MaMon = chiTiet.MaMon,
                    TenMon = chiTiet.TenMon,
                    SoLuongCt = chiTiet.SoLuongCt,
                    ThanhTien = chiTiet.ThanhTien,
                    HinhAnhHd = chiTiet.HinhAnhHd

                    // Điền các thông tin chi tiết hoá đơn từ Model khác
                };
                _context.ChiTietHds.Add(chiTietHoaDon);
                var monAn = _context.MonAns.SingleOrDefault(ma => ma.MaMon == chiTiet.MaMon);
                if (monAn != null)
                {
                    monAn.SoLuong -= chiTiet.SoLuongCt;
                    monAn.SoLuongDaBan += chiTiet.SoLuongCt;
                }
            }

            _context.SaveChanges();



            // Xóa dữ liệu giỏ hàng của người dùng sau khi đã lưu vào hoá đơn
            HttpContext.Session.Remove("GioHang");


            var donHang = new DonHang
            {
                MaDonHang = maHoaDon,
                NgayDatHang = DateTime.Now,
                TrangThaiDh = 1,
                MaKH = khachHang.MaKH,
                MaHd = maHoaDon,
                NguoiNhan = model.NguoiNhan,
                SDTNN = model.SDTNN,
                DiaChiNhan = model.DiaChiNhan,
                GhiChu = model.GhiChu

            };

            _context.DonHangs.Add(donHang);
            _context.SaveChanges();


            foreach (var chiTiet in danhSachMonAn)
            {
                var chiTietDonHang = new ChiTietDh
                {
                    MaDonHang = maHoaDon, // Gán mã đơn hàng
                    MaMon = chiTiet.MaMon,
                    SoLuongMmdh = chiTiet.SoLuongCt,
                    TenMonAnDh = chiTiet.TenMon,
                    HinhAnhCt = chiTiet.HinhAnhHd
                };
                _context.ChiTietDhs.Add(chiTietDonHang);


            }
            _context.SaveChanges();

            var thongbao = new ThongBao
            {
                MaTB = maHoaDon,
                NoiDung = $"Vừa có đơn hàng mới chờ xét duyệt - Mã đơn hàng: {maHoaDon}",
                MaHD = maHoaDon,
                ThoiGian = DateTime.Now,
                TrangThaiTB = 1

            };
            _context.ThongBaos.Add(thongbao);
            _context.SaveChanges();

            if (model.HinhThucTT == "Chuyển khoản")
            {
                var paymentModel = new PaymentInformationModel
                {
                    OrderType = "Laptop",
                    Amount = TienTt, // Số tiền cần thanh toán

                    OrderDescription = "Thanh toán đơn hàng" + hoaDon.MaHd, // Mô tả đơn hàng
                    Name = khachHang.TenKh // Tên khách hàng (hoặc thông tin khác cần thiết)
                };

                // Gọi IVnPayService để tạo URL thanh toán VNPay
                var paymentUrl = _vnPayService.CreatePaymentUrl(paymentModel, HttpContext);

                var mailData1 = new MailData
                {
                    ReceiverName = khachHang.TenKh, // Thay "Tên khách hàng" bằng tên thực sự của khách hàng
                    ReceiverEmail = khachHang.EmailKh, // Thay "email@example.com" bằng địa chỉ email thực sự của khách hàng
                    Title = "Thông báo đặt hàng thành công",
                    Body = "Chào " + khachHang.TenKh + ",\n\n"
            + "Đơn hàng của bạn đã được xác nhận thành công. Dưới đây là chi tiết đơn hàng:\n\n"
            + "Mã đơn hàng: " + hoaDon.MaHd + "\n"
            + "Ngày đặt hàng: " + hoaDon.NgayHd.ToString("dd/MM/yyyy") + "\n"
            + "Danh sách sản phẩm:\n"
                };

                foreach (var chiTiet in danhSachMonAn)
                {
                    mailData1.Body += "Tên sản phẩm: " + chiTiet.TenMon + "\n"
                                     + "Số lượng: " + chiTiet.SoLuongCt + "\n"
                                     + "Thành tiền: " + chiTiet.ThanhTien.ToString("N0") + "" + "VNĐ" + "\n";

                }

                mailData1.Body += "Tổng tiền: " + hoaDon.TongTien.ToString("N0") + "" + "VNĐ" + "\n"
                                 + "Tổng tiền giảm: " + hoaDon.TienGiam.ToString("N0") + "" + "VNĐ" + "\n"
                                 + "Tổng tiền thanh toán: " + hoaDon.TienTt.ToString("N0") + "" + "VNĐ" + "\n\n"
                                 + "Cảm ơn bạn đã mua hàng tại cửa hàng chúng tôi.";

                // Gửi email
                _mailService.SendMail(mailData1);
                // Chuyển hướng sang trang thanh toán của VNPay
                return Redirect(paymentUrl);
            }


            if (model.HinhThucTT == "Chuyển khoản PayPal")
            {
                var paymentModel = new PaymentInformationModel1
                {
                    OrderType = "Laptop",
                    Amount = TienTt, // Số tiền cần thanh toán

                    OrderDescription = "Thanh toán đơn hàng" + hoaDon.MaHd, // Mô tả đơn hàng
                    Name = khachHang.TenKh // Tên khách hàng (hoặc thông tin khác cần thiết)
                };

                // Gọi IVnPayService để tạo URL thanh toán VNPay
                var paymentUrl = _payPalService.CreatePaymentUrl(paymentModel, HttpContext);
                return Redirect(paymentUrl);
            }

                var mailData = new MailData
            {
                ReceiverName = khachHang.TenKh, // Thay "Tên khách hàng" bằng tên thực sự của khách hàng
                ReceiverEmail = khachHang.EmailKh, // Thay "email@example.com" bằng địa chỉ email thực sự của khách hàng
                Title = "Thông báo đặt hàng thành công",
                Body = "Chào " + khachHang.TenKh + ",\n\n"
            + "Đơn hàng của bạn đã được xác nhận thành công. Dưới đây là chi tiết đơn hàng:\n\n"
            + "Mã đơn hàng: " + hoaDon.MaHd + "\n"
            + "Ngày đặt hàng: " + hoaDon.NgayHd.ToString("dd/MM/yyyy") + "\n"
            + "Danh sách sản phẩm:\n"
            };

            foreach (var chiTiet in danhSachMonAn)
            {
                mailData.Body += "Tên sản phẩm: " + chiTiet.TenMon + "\n"
                                 + "Số lượng: " + chiTiet.SoLuongCt + "\n"
                                 + "Thành tiền: " + chiTiet.ThanhTien.ToString("N0")+  "" + "VNĐ" + "\n";
                                 
            }

            mailData.Body += "Tổng tiền: " + hoaDon.TongTien.ToString("N0") + ""  +"VNĐ" + "\n"
                             + "Tổng tiền giảm: " + hoaDon.TienGiam.ToString("N0") + ""  + "VNĐ" + "\n"
                             + "Tổng tiền thanh toán: " + hoaDon.TienTt.ToString("N0") + "" +  "VNĐ" + "\n\n"
                             + "Cảm ơn bạn đã mua hàng tại cửa hàng chúng tôi.";

            // Gửi email
            _mailService.SendMail(mailData);


            // Chuyển người dùng đến trang cảm ơn hoặc trang xác nhận hoá đơn
            return Redirect("/GioHang");


        }

        public int GenerateRandomCustomerCode()
        {
            Random random = new Random();
            int customerCode;

            do
            {
                customerCode = random.Next(1000, 9999); // Sinh số ngẫu nhiên có 4 chữ số
            } while (_context.HoaDons.Any(kh => kh.MaHd == customerCode)); // Kiểm tra xem mã đã tồn tại trong CSDL hay chưa

            return customerCode;
        }

        [HttpPost]
        public IActionResult SendMail(MailData mailData)
        {
            _mailService.SendMail(mailData);
            return View();
        }

        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }


    }
}

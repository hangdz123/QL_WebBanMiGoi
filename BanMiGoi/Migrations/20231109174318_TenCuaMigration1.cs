using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThanhThoaiRestaurant.Migrations
{
    public partial class initials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BanAn",
                columns: table => new
                {
                    MaBan = table.Column<string>(type: "char(10)", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SucChua = table.Column<int>(type: "int", nullable: false),
                    TrangThaiBA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanAn", x => x.MaBan);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChiTietTinNhan",
                columns: table => new
                {
                    MaTinNhan = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    TenDangNhap = table.Column<string>(type: "char(10)", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NoiDung = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ThoiGian = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietTinNhan", x => new { x.MaTinNhan, x.TenDangNhap });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CPU",
                columns: table => new
                {
                    MaCPU = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenLoaiCPU = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPU", x => x.MaCPU);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    MaDanhGia = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MaMon = table.Column<int>(type: "int", fixedLength: true, nullable: false),
                    TenDangNhap = table.Column<string>(type: "char", fixedLength: true, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NoiDung = table.Column<string>(type: "char(200)", fixedLength: true, maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NgayDG = table.Column<DateTime>(type: "datetime", nullable: false),
                    Diem = table.Column<int>(type: "int", fixedLength: true, nullable: false),
                    HinhAnh1 = table.Column<string>(type: "char", fixedLength: true, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HinhAnh2 = table.Column<string>(type: "char", fixedLength: true, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HinhAnh3 = table.Column<string>(type: "char", fixedLength: true, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HinhAnh4 = table.Column<string>(type: "char", fixedLength: true, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HinhAnh5 = table.Column<string>(type: "char", fixedLength: true, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Video = table.Column<string>(type: "char", fixedLength: true, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGia", x => x.MaDanhGia);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ManHinh",
                columns: table => new
                {
                    MaMH = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KichThuoc = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManHinh", x => x.MaMH);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    TenDangNhap = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MatKhau = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailND = table.Column<string>(type: "char(50)", fixedLength: true, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VaiTro = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung_1", x => x.TenDangNhap);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NhomMonAn",
                columns: table => new
                {
                    MaNhom = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenNhom = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhomMonAn", x => x.MaNhom);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OCung",
                columns: table => new
                {
                    MaOC = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DungLuong = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCung", x => x.MaOC);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PhieuGiamGia",
                columns: table => new
                {
                    MaPhieuGG = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MoTa = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhanTram = table.Column<int>(type: "int", nullable: false),
                    LoaiMa = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SoLuongPhieu = table.Column<int>(type: "int", nullable: false),
                    Diem = table.Column<int>(type: "int", nullable: false),
                    TrangThaiPGG = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuGiamGia", x => x.MaPhieuGG);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RAM",
                columns: table => new
                {
                    MaRam = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DungLuongRam = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAM", x => x.MaRam);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TinNhan",
                columns: table => new
                {
                    MaTinNhan = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinNhan", x => x.MaTinNhan);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKH = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenKH = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NgayTG = table.Column<DateTime>(type: "datetime", nullable: false),
                    DoanhSo = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NgaySinhKH = table.Column<DateTime>(type: "date", nullable: false),
                    GioiTinhKH = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailKH = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SDTKH = table.Column<string>(type: "char(20)", fixedLength: true, maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiaChiKH = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiemTichLuy = table.Column<int>(type: "int", nullable: false),
                    TenDangNhap = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.MaKH);
                    table.ForeignKey(
                        name: "fk_TenDN1",
                        column: x => x.TenDangNhap,
                        principalTable: "NguoiDung",
                        principalColumn: "TenDangNhap");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    MaNV = table.Column<string>(type: "char(10)", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TenNV = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NgayVL = table.Column<DateTime>(type: "date", nullable: false),
                    SDTNV = table.Column<string>(type: "char(15)", fixedLength: true, maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailNV = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GioiTinhNV = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiaChiNV = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChucVu = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NgaySinhNV = table.Column<DateTime>(type: "date", nullable: false),
                    CCCDNV = table.Column<string>(type: "char(20)", fixedLength: true, maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TenDangNhap = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HinhAnh = table.Column<string>(type: "char(20)", fixedLength: true, maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.MaNV);
                    table.ForeignKey(
                        name: "fk_TenDN",
                        column: x => x.TenDangNhap,
                        principalTable: "NguoiDung",
                        principalColumn: "TenDangNhap");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    MaHD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NgayHD = table.Column<DateTime>(type: "datetime", nullable: false),
                    TongTien = table.Column<double>(type: "double", nullable: false),
                    TienGiam = table.Column<double>(type: "double", nullable: false),
                    TienTT = table.Column<double>(type: "double", nullable: false),
                    MaPhieuGG = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    HinhThucTT = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrangThaiHD = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.MaHD);
                    table.ForeignKey(
                        name: "fk_HoaDon_PGG",
                        column: x => x.MaPhieuGG,
                        principalTable: "PhieuGiamGia",
                        principalColumn: "MaPhieuGG");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MonAn",
                columns: table => new
                {
                    MaMon = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenMon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GiaBan = table.Column<double>(type: "double", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    SoLuongDaBan = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    DonViTinh = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MoTaNgan = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MoTaDai = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HinhAnh = table.Column<string>(type: "char(20)", fixedLength: true, maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HinhAnh1 = table.Column<string>(type: "char(20)", fixedLength: true, maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HinhAnh2 = table.Column<string>(type: "char(20)", fixedLength: true, maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HinhAnh3 = table.Column<string>(type: "char(20)", fixedLength: true, maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaNhom = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    TenNhom = table.Column<string>(type: "char(20)", fixedLength: true, maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GiaGoc = table.Column<double>(type: "double", nullable: false),
                    GiaKhuyenMai = table.Column<double>(type: "double", nullable: false),
                    TrangThaiMA = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    GhiChu = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NgayMoBan = table.Column<DateTime>(type: "datetime", nullable: false),
                    MaOC = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    MaRam = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    MaMH = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    MaCPU = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonAn", x => x.MaMon);
                    table.ForeignKey(
                        name: "fk_MaNhom",
                        column: x => x.MaNhom,
                        principalTable: "NhomMonAn",
                        principalColumn: "MaNhom",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonAn_CPU_MaCPU",
                        column: x => x.MaCPU,
                        principalTable: "CPU",
                        principalColumn: "MaCPU",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonAn_ManHinh_MaMH",
                        column: x => x.MaMH,
                        principalTable: "ManHinh",
                        principalColumn: "MaMH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonAn_OCung_MaOC",
                        column: x => x.MaOC,
                        principalTable: "OCung",
                        principalColumn: "MaOC",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonAn_RAM_MaRam",
                        column: x => x.MaRam,
                        principalTable: "RAM",
                        principalColumn: "MaRam",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DatBan",
                columns: table => new
                {
                    MaLichHen = table.Column<string>(type: "char(10)", fixedLength: true, maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TenKH = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SoKhach = table.Column<int>(type: "int", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayHen = table.Column<DateTime>(type: "datetime", nullable: false),
                    TrangThaiLH = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaBan = table.Column<string>(type: "char(10)", fixedLength: true, maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaKH = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_DatBan_BanAn",
                        column: x => x.MaBan,
                        principalTable: "BanAn",
                        principalColumn: "MaBan");
                    table.ForeignKey(
                        name: "fk_DatBan_Khachhang",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    MaGioHang = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SoLuongMon = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<float>(type: "float", nullable: false),
                    TongGiamGia = table.Column<float>(type: "float", nullable: false),
                    TienThanhToan = table.Column<float>(type: "float", nullable: false),
                    KhachHangMaKH = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.MaGioHang);
                    table.ForeignKey(
                        name: "FK_GioHang_KhachHang_KhachHangMaKH",
                        column: x => x.KhachHangMaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    MaDonHang = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NgayDatHang = table.Column<DateTime>(type: "datetime", nullable: false),
                    TrangThaiDH = table.Column<int>(type: "int", nullable: false),
                    PhiVanChuyen = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MaKH = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    MaHD = table.Column<int>(type: "int", fixedLength: true, nullable: false),
                    NguoiNhan = table.Column<string>(type: "char(50)", fixedLength: true, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SDTNN = table.Column<string>(type: "char(10)", fixedLength: true, maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiaChiNhan = table.Column<string>(type: "char(10)", fixedLength: true, maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GhiChu = table.Column<string>(type: "char(10)", fixedLength: true, maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.MaDonHang);
                    table.ForeignKey(
                        name: "fk_DonHang_HoaDon",
                        column: x => x.MaHD,
                        principalTable: "HoaDon",
                        principalColumn: "MaHD");
                    table.ForeignKey(
                        name: "fk_DonHang_Khachhang",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PhieuGoiMon",
                columns: table => new
                {
                    MaPhieuGM = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenMonAnPGM = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NgayGM = table.Column<DateTime>(type: "datetime", nullable: false),
                    MaBan = table.Column<string>(type: "char(10)", fixedLength: true, maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaHD = table.Column<int>(type: "int", fixedLength: true, nullable: false),
                    GhiChu = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuGoiMon", x => x.MaPhieuGM);
                    table.ForeignKey(
                        name: "fk_PhieuGoiMon_BanAn",
                        column: x => x.MaBan,
                        principalTable: "BanAn",
                        principalColumn: "MaBan");
                    table.ForeignKey(
                        name: "fk_PhieuGoiMon_HoaDon",
                        column: x => x.MaHD,
                        principalTable: "HoaDon",
                        principalColumn: "MaHD");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ThongBao",
                columns: table => new
                {
                    MaTB = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NoiDung = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaHD = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime", nullable: false),
                    TrangThaiTB = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBao", x => x.MaTB);
                    table.ForeignKey(
                        name: "FK_ThongBao_HoaDon",
                        column: x => x.MaHD,
                        principalTable: "HoaDon",
                        principalColumn: "MaHD",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChiTietHD",
                columns: table => new
                {
                    MaChiTietHd = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MaMon = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    MaHD = table.Column<int>(type: "int", fixedLength: true, nullable: false),
                    SoLuongCT = table.Column<int>(type: "int", nullable: false),
                    ThanhTien = table.Column<double>(type: "double", nullable: false),
                    TenMon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HinhAnhHd = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTHD", x => x.MaChiTietHd);
                    table.ForeignKey(
                        name: "fk_HD_CTHD",
                        column: x => x.MaHD,
                        principalTable: "HoaDon",
                        principalColumn: "MaHD");
                    table.ForeignKey(
                        name: "fk_MonAn_CTHD",
                        column: x => x.MaMon,
                        principalTable: "MonAn",
                        principalColumn: "MaMon");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChiTietGH",
                columns: table => new
                {
                    MaMon = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    MaGioHang = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    SoLuongMM = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TenMon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HinhAnh = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GiaBan = table.Column<float>(type: "float", nullable: false),
                    GiaKhuyenMai = table.Column<float>(type: "float", nullable: false),
                    ThanhTien = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTGH", x => new { x.MaMon, x.MaGioHang });
                    table.ForeignKey(
                        name: "fk_GH1_CTGH",
                        column: x => x.MaGioHang,
                        principalTable: "GioHang",
                        principalColumn: "MaGioHang");
                    table.ForeignKey(
                        name: "fk_MA_CTGH",
                        column: x => x.MaMon,
                        principalTable: "MonAn",
                        principalColumn: "MaMon");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChiTietDH",
                columns: table => new
                {
                    MaChiTietDh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MaMon = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    MaDonHang = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    TenMonAnDH = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SoLuongMMDH = table.Column<int>(type: "int", nullable: false),
                    HinhAnhCt = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DH", x => x.MaChiTietDh);
                    table.ForeignKey(
                        name: "fk_DH_CTDH",
                        column: x => x.MaDonHang,
                        principalTable: "DonHang",
                        principalColumn: "MaDonHang");
                    table.ForeignKey(
                        name: "fk_MonAn_CTDH",
                        column: x => x.MaMon,
                        principalTable: "MonAn",
                        principalColumn: "MaMon");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChiTietGM",
                columns: table => new
                {
                    MaMon = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    MaPhieuGM = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    SoLuongCT1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTGM", x => new { x.MaMon, x.MaPhieuGM });
                    table.ForeignKey(
                        name: "fk_MonAn_CTGM",
                        column: x => x.MaMon,
                        principalTable: "MonAn",
                        principalColumn: "MaMon");
                    table.ForeignKey(
                        name: "fk_PGM_CTGM",
                        column: x => x.MaPhieuGM,
                        principalTable: "PhieuGoiMon",
                        principalColumn: "MaPhieuGM");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDH_MaDonHang",
                table: "ChiTietDH",
                column: "MaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDH_MaMon",
                table: "ChiTietDH",
                column: "MaMon");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGH_MaGioHang",
                table: "ChiTietGH",
                column: "MaGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGM_MaPhieuGM",
                table: "ChiTietGM",
                column: "MaPhieuGM");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHD_MaHD",
                table: "ChiTietHD",
                column: "MaHD");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHD_MaMon",
                table: "ChiTietHD",
                column: "MaMon");

            migrationBuilder.CreateIndex(
                name: "IX_DatBan_MaBan",
                table: "DatBan",
                column: "MaBan");

            migrationBuilder.CreateIndex(
                name: "IX_DatBan_MaKH",
                table: "DatBan",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaHD",
                table: "DonHang",
                column: "MaHD");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaKH",
                table: "DonHang",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_KhachHangMaKH",
                table: "GioHang",
                column: "KhachHangMaKH");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaPhieuGG",
                table: "HoaDon",
                column: "MaPhieuGG");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_TenDangNhap",
                table: "KhachHang",
                column: "TenDangNhap");

            migrationBuilder.CreateIndex(
                name: "IX_MonAn_MaCPU",
                table: "MonAn",
                column: "MaCPU");

            migrationBuilder.CreateIndex(
                name: "IX_MonAn_MaMH",
                table: "MonAn",
                column: "MaMH");

            migrationBuilder.CreateIndex(
                name: "IX_MonAn_MaNhom",
                table: "MonAn",
                column: "MaNhom");

            migrationBuilder.CreateIndex(
                name: "IX_MonAn_MaOC",
                table: "MonAn",
                column: "MaOC");

            migrationBuilder.CreateIndex(
                name: "IX_MonAn_MaRam",
                table: "MonAn",
                column: "MaRam");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_TenDangNhap",
                table: "NhanVien",
                column: "TenDangNhap");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuGoiMon_MaBan",
                table: "PhieuGoiMon",
                column: "MaBan");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuGoiMon_MaHD",
                table: "PhieuGoiMon",
                column: "MaHD");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBao_MaHD",
                table: "ThongBao",
                column: "MaHD");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDH");

            migrationBuilder.DropTable(
                name: "ChiTietGH");

            migrationBuilder.DropTable(
                name: "ChiTietGM");

            migrationBuilder.DropTable(
                name: "ChiTietHD");

            migrationBuilder.DropTable(
                name: "ChiTietTinNhan");

            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "DatBan");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "ThongBao");

            migrationBuilder.DropTable(
                name: "TinNhan");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "PhieuGoiMon");

            migrationBuilder.DropTable(
                name: "MonAn");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "BanAn");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "NhomMonAn");

            migrationBuilder.DropTable(
                name: "CPU");

            migrationBuilder.DropTable(
                name: "ManHinh");

            migrationBuilder.DropTable(
                name: "OCung");

            migrationBuilder.DropTable(
                name: "RAM");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "PhieuGiamGia");
        }
    }
}

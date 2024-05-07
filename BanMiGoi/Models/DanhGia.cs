﻿namespace ThanhThoaiRestaurant.Models
{
    public class DanhGia
    {
        public int MaDanhGia { get;set; }
        public int MaMon { get;set;}
        public string TenDangNhap { get;set; }
        public string NoiDung { get;set;}
        public DateTime NgayDG { get;set; }
        public int Diem { get; set; }

		public string HinhAnh1 { get; set; }
		public string HinhAnh2 { get; set; }
		public string HinhAnh3 { get; set; }
		public string HinhAnh4 { get; set; }
		public string HinhAnh5 { get; set; }
		public string Video { get; set; }
	}
}

using System;

namespace Entities
{
    public class Kho
    {
        // Thuộc tính
        public string? TenMatHang { get; set; }
        public int SoLuong;
        public DateOnly NgaySanXuat;
        public DateOnly HanDung;

        // Constructor mặc định
        public Kho(string tenMatHang, int soLuong, DateOnly ngaySanXuat, DateOnly hanDung)
        {
            TenMatHang = tenMatHang;
            SoLuong = soLuong;
            NgaySanXuat = ngaySanXuat;
            HanDung = hanDung;
        }
    }
}
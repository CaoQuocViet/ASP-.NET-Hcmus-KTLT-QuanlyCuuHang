using System;

namespace Entities
{
    public class Kho
    {
        // Thuộc tính
        public string? TenMatHang { get; set; }
        public int SoLuong { get; set; }
        public DateOnly NgaySanXuat { get; set; }
        public DateOnly HanDung { get; set; }

        // Constructor không tham số
        public Kho(){}

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
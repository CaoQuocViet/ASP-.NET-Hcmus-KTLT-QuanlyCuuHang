using System;

namespace Entities
{
    public class Kho
    {
        public string? TenMatHang { get; set; }
        public int SoLuong;
        public DateOnly NgaySanXuat;
        public DateOnly HanDung;

        public Kho(string tenMatHang, int soLuong, DateOnly ngaySanXuat, DateOnly hanDung)
        {
            TenMatHang = tenMatHang;
            SoLuong = soLuong;
            NgaySanXuat = ngaySanXuat;
            HanDung = hanDung;
        }
    }
}
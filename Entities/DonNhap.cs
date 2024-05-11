using System;
using System.Collections.Generic;

namespace Entities
{
    public class DonNhap
    {
        // Thuộc tính
        public string MaSo { get; set; }
        public DateOnly Ngay { get; set; }
        public List<Kho> Kho { get; set; }

        // Constructor mặc định
        public DonNhap(string maSo, DateOnly ngay, List<Kho> kho)
        {
            MaSo = maSo;
            Ngay = ngay;
            Kho = kho;
        }
    }
}
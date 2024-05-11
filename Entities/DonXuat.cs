
using System;
using System.Collections.Generic;

namespace Entities
{
    public class DonXuat
    {
        // Thuộc tính
        public string MaSo { get; set; }
        public DateOnly Ngay { get; set; }
        public List<Kho> Kho { get; set; }

        // Constructor mặc định
        public DonXuat(string maSo, DateOnly ngay, List<Kho> kho)
        {
            MaSo = maSo;
            Ngay = ngay;
            Kho = kho;
        }
    }
}
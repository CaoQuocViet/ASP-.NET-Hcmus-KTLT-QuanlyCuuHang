using System;
using System.Collections.Generic;

namespace Entities
{
    public class DonNhap
    {
        public string MaSo { get; set; }
        public DateOnly Ngay { get; set; }
        public List<Kho> Kho { get; set; }

        public DonNhap(string maSo, DateOnly ngay, List<Kho> kho)
        {
            MaSo = maSo;
            Ngay = ngay;
            Kho = kho;
        }
    }
}
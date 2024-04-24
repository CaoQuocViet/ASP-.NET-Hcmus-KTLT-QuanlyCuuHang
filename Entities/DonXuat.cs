
using System;
using System.Collections.Generic;

namespace Entities
{
    public class DonXuat
    {
        public string MaSo { get; set; }
        public DateOnly Ngay { get; set; }
        public List<Kho> Kho { get; set; }

        public DonXuat(string maSo, DateOnly ngay, List<Kho> kho)
        {
            MaSo = maSo;
            Ngay = ngay;
            Kho = kho;
        }
    }
}
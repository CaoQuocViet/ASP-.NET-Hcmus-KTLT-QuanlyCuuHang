using System;
using System.Collections.Generic;

namespace Entities
{
    public class DonNhap
    {
        public string MaSo { get; set; }
        public DateOnly Ngay { get; set; }
        public List<Kho> Kho { get; set; }

        public DonNhap()
        {
            MaSo = string.Empty;
            Ngay = default;
            Kho = new List<Kho>();
        }
    }
}
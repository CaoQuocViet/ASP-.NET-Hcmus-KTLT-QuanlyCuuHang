using System;

namespace Entities
{
    public class LoaiHang
    {
        public string MaSo { get; set; } = string.Empty;
        public string Ten { get; set; } = string.Empty;

        public LoaiHang(string maSo, string ten)
        {
            MaSo = maSo;
            Ten = ten;
        }
    }
}

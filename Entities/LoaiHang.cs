using System;

namespace Entities
{
    public class LoaiHang
    {
        //Thuộc tính
        public string MaSo { get; set; } = string.Empty;
        public string Ten { get; set; } = string.Empty;

        //Constructor mặc định
        public LoaiHang()
        {
        }
        
        //Constructor có tham số
        public LoaiHang(string maSo, string ten)
        {
            MaSo = maSo;
            Ten = ten;
        }
    }
}

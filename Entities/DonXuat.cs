using System;

namespace Entities
{
    public class DonXuat
    {
        public string MaSo { get; set; }
        public DateOnly Ngay { get; set; }
        public Kho[] Kho { get; set; }

        public DonXuat(string maSo, DateOnly ngay, Kho[] kho)
        {
            MaSo = maSo;
            Ngay = ngay;
            Kho = kho;
        }
    }
}
using System;

namespace Entities
{
    public class DonNhap
    {
        public string MaSo { get; set; }
        public DateOnly Ngay { get; set; }
        public Kho[] Kho { get; set; }

        public DonNhap()
        {
            MaSo = string.Empty;
            Ngay = default;
            Kho = new Kho[0];
        }
    }
}
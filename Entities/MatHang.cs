namespace Entities
{
    public class MatHang
    {
        public string MaSo { get; set; }
        public string Ten { get; set; }
        public string LoaiHang { get; set; }
        public string ThuongHieu { get; set; }
        public int Gia { get; set; }

        public MatHang(){}

        public MatHang(string maSo, string ten, string loaiHang, string thuongHieu, int gia)
        {
            MaSo = maSo;
            Ten = ten;
            LoaiHang = loaiHang;
            ThuongHieu = thuongHieu;
            Gia = gia;
        }
    }
}
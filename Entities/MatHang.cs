namespace Entities
{
    public class MatHang
    {
        // Thuộc tính
        public string MaSo { get; set; }
        public string Ten { get; set; }
        public string LoaiHang { get; set; }
        public string ThuongHieu { get; set; }
        public int Gia { get; set; }

        // Constructor mặc định
        public MatHang()
        {
            MaSo = default!;
            Ten = default!;
            LoaiHang = default!;
            ThuongHieu = default!;
            Gia = default;
        }

        // Constructor có tham số
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
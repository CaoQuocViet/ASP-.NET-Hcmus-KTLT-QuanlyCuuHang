using Repo;
using Entities;



namespace Service
{
    public class XL_LoaiHang : IXL_LoaiHang
    {
        private ILT_LoaiHang _luuTruLoaiHang = new LT_LoaiHang();
        private const int _maSoMaxLength = 30;
        private const int _tenMaxLength = 50;
        public LoaiHang Empty()
        {
            return new LoaiHang("", "");
        }

        public LoaiHang[] DocDanhSach(string sKeyword)
        {
            return _luuTruLoaiHang.DocDanhSach(sKeyword);
        }

        public string Them(LoaiHang loaihang)
        {
            if (loaihang.MaSo.Length > _maSoMaxLength)
            {
                return "Mã loại hàng không hợp lệ";
            }
            if (loaihang.Ten.Length > _tenMaxLength)
            {
                return "Tên loại hàng không hợp lệ";
            }

            LT_LoaiHang ltLoaiHang = new();
            return _luuTruLoaiHang.Them(loaihang);
        }

        public string Sua(string sMaSo, string sTen, ref LoaiHang loaihangOld)
        {
            LoaiHang loaihang = new LoaiHang(sMaSo, sTen);

            string sInfo = _luuTruLoaiHang.Sua(loaihangOld, loaihang);
            if (string.IsNullOrEmpty(sInfo))
            {
                XL_MatHang matHang = new XL_MatHang(); 
                matHang.CapNhatLoaiHang(loaihangOld, loaihang);  
                loaihangOld = loaihang;
            }

            return sInfo;
        }

        public string Xoa(string sMaSo, string sTen)
        {
            LoaiHang loaihang = new LoaiHang(sMaSo, sTen);  

            XL_MatHang xlMatHang = new XL_MatHang();
            MatHang[] DSmathang = xlMatHang.DocDanhSach("");
            for (int i = 0; i < DSmathang.Length; i++)
            {
                if (loaihang.Ten == DSmathang[i].LoaiHang)
                {
                    XL_DonNhap xlDonNhap = new XL_DonNhap();  
                    if (string.IsNullOrEmpty(xlDonNhap.MatHangTonTai(DSmathang[i]))) 
                    {
                        return "Không thể xóa loại hàng có mặt hàng được sử dụng trong đơn nhập";
                    }

                    XL_DonXuat xlDonXuat = new XL_DonXuat(); 
                    if (string.IsNullOrEmpty(xlDonXuat.MatHangTonTai(DSmathang[i])))  
                    {
                        return "Không thể xóa loại hàng có mặt hàng được sử dụng trong đơn xuất";
                    }
                }
            }

            return _luuTruLoaiHang.Xoa(loaihang);
        }

        public LoaiHang? ReadInfo(string loaihangCode)
        {
            return _luuTruLoaiHang.ReadInfo(loaihangCode);
        }
    }
}

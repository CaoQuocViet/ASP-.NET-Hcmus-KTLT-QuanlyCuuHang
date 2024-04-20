using Repo;
using Entities;

namespace Service
{
    public class XL_MatHang : IXL_MatHang
    {
        private ILT_MatHang _luuTruMatHang = new LT_MatHang();
        private const int MaSoMaxLength = 30;
        public MatHang[] DocDanhSach(string sKeyword)
        {
            return _luuTruMatHang.DocDanhSach(sKeyword);  
        }

        public void CapNhatLoaiHang(LoaiHang loaihangOld, LoaiHang loaihangNew)
        {
            _luuTruMatHang.CapNhatLoaiHang(loaihangOld, loaihangNew);  
        }

        public string Them(string sMaSo, string sTen, string sLoaiHang, string sGia, ref MatHang mathang)
        {
            mathang.MaSo = sMaSo;
            mathang.Ten = sTen;
            mathang.LoaiHang = sLoaiHang;

            if (mathang.MaSo.Length == 0 || mathang.MaSo.Length > MaSoMaxLength)
            {
                return "Mã mặt hàng không hợp lệ";
            }

            XL_LoaiHang xlLoaiHang = new XL_LoaiHang();  
            LoaiHang[] DSloaihang = xlLoaiHang.DocDanhSach("");  
            for (int i = 0; i < DSloaihang.Length; i++)
            {
                if (mathang.LoaiHang == DSloaihang[i].Ten)
                {
                    break;
                }
            }
            int GiaMaxValue = 1000;  

            int parsedGia;
            if (!int.TryParse(sGia, out parsedGia))
            {
                return "Giá mặt hàng không hợp lệ";
            }
            mathang.Gia = parsedGia;
            if (mathang.Gia == 0 || mathang.Gia > GiaMaxValue)
            {
                return "Giá mặt hàng không hợp lệ";
            }

             
            return _luuTruMatHang.Them(mathang); 
        }

        public string Sua(string sMaSo, string sTen, string sLoaiHang, string sGia, string sThuongHieu, ref MatHang mathangOld)
        {
            MatHang mathang = new MatHang(sMaSo, sTen, sLoaiHang, sThuongHieu, 0)
            {
                MaSo = sMaSo,
                Ten = sTen,
                LoaiHang = sLoaiHang,
                ThuongHieu = sThuongHieu
            };

            if (mathang.MaSo.Length == 0 || mathang.MaSo.Length > MaSoMaxLength)
            {
                return "Mã mặt hàng không hợp lệ";
            }

            XL_LoaiHang xlLoaiHang = new XL_LoaiHang(); 
            LoaiHang[] DSloaihang = xlLoaiHang.DocDanhSach(""); 
            bool isValid = false;
            for (int i = 0; i < DSloaihang.Length; i++)
            {
                if (mathang.LoaiHang == DSloaihang[i].Ten)
                {
                    isValid = true;
                    break;
                }
            }
            if (!isValid)
            {
                return "Loại hàng không tồn tại";
            }

            int parsedGia;
            if (!int.TryParse(sGia, out parsedGia))
            {
                return "Giá mặt hàng không hợp lệ";
            }
            mathang.Gia = parsedGia;
            string sInfo = ""; 
            if (string.IsNullOrEmpty(sInfo))
            {
                XL_Kho kho = new XL_Kho(); 
                kho.CapNhatDS(mathangOld, mathang); 
                XL_DonNhap xlDonNhap = new XL_DonNhap(); 
                xlDonNhap.CapNhatDS(mathangOld, mathang); 
                XL_DonXuat xlDonXuat = new XL_DonXuat(); 
                xlDonXuat.CapNhatDS(mathangOld, mathang); 
                mathangOld = mathang;
            }

            return sInfo;
        }

        public string Xoa(string sMaSo, string sTen, string sLoaiHang, string sThuongHieu, string sGia)
        {
            MatHang mathang = new MatHang(sMaSo, sTen, sLoaiHang, sThuongHieu, 0)
            {
                MaSo = sMaSo,
                Ten = sTen,
                LoaiHang = sLoaiHang,
                ThuongHieu = sThuongHieu
            };

            if (mathang.MaSo.Length == 0 || mathang.MaSo.Length > MaSoMaxLength)
            {
                return "Mã mặt hàng không hợp lệ";
            }

            XL_LoaiHang xlLoaiHang = new XL_LoaiHang();  
            LoaiHang[] DSloaihang = xlLoaiHang.DocDanhSach("");  
            for (int i = 0; i < DSloaihang.Length; i++)
            {
                if (mathang.LoaiHang == DSloaihang[i].Ten)
                {
                    break;
                }
            }
            int parsedGia;
            if (!int.TryParse(sGia, out parsedGia))
            {
                return "Giá mặt hàng không hợp lệ";
            }
            mathang.Gia = parsedGia;
            if (string.IsNullOrEmpty(new XL_DonNhap().MatHangTonTai(mathang)))
            {
                return "Không thể xóa mặt hàng được sử dụng trong đơn nhập";
            }

            XL_DonXuat xlDonXuat = new XL_DonXuat();  
            if (string.IsNullOrEmpty(xlDonXuat.MatHangTonTai(mathang)))
            {
                return "Không thể xóa mặt hàng được sử dụng trong đơn xuất";
            }

             
            return _luuTruMatHang.Xoa(mathang);
        }

        public MatHang? ReadInfo(string sMatHangMaSo)
        {
             
            return _luuTruMatHang.ReadInfo(sMatHangMaSo);  
        }
    }
}

using CaoQuocViet.XuLyLuuTru;
using CaoQuocViet.Entities;

namespace CaoQuocViet.XuLyNghiepVu
{
    public class XL_MatHang
    {
        public const int MaSoMaxLength = 30;
        public const int TenMaxLength = 30;
        public const int GiaMaxValue = 100000000;

        public static MatHang[] DocDanhSach(string sKeyword)
        {
            return LT_MatHang.DocDanhSach(sKeyword);
        }

        public static void CapNhatLoaiHang(LoaiHang loaihangOld, LoaiHang loaihangNew)
        {
            LT_MatHang.CapNhatLoaiHang(loaihangOld, loaihangNew);
        }

        public static string Them(string sMaSo, string sTen, string sLoaiHang, string sGia, ref MatHang mathang)
        {
            mathang.MaSo = sMaSo;
            mathang.Ten = sTen;
            mathang.LoaiHang = sLoaiHang;

            if (mathang.MaSo.Length == 0 || mathang.MaSo.Length > MaSoMaxLength)
            {
                return "Mã mặt hàng không hợp lệ";
            }

            if (mathang.Ten.Length == 0 || mathang.Ten.Length > TenMaxLength)
            {
                return "Tên mặt hàng không hợp lệ";
            }

            LoaiHang[] DSloaihang = XL_LoaiHang.DocDanhSach("");
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

            if (!int.TryParse(sGia, out mathang.Gia))
            {
                return "Giá mặt hàng không hợp lệ";
            }
            if (mathang.Gia == 0 || mathang.Gia > GiaMaxValue)
            {
                return "Giá mặt hàng không hợp lệ";
            }

            return LT_MatHang.Them(mathang);
        }

        public static string Sua(string sMaSo, string sTen, string sLoaiHang, string sGia, string sThuongHieu, ref MatHang mathangOld)
        {
            MatHang mathang;
            mathang.MaSo = sMaSo;
            mathang.Ten = sTen;
            mathang.LoaiHang = sLoaiHang;
            mathang.ThuongHieu = sThuongHieu;

            if (mathang.MaSo.Length == 0 || mathang.MaSo.Length > MaSoMaxLength)
            {
                return "Mã mặt hàng không hợp lệ";
            }

            if (mathang.Ten.Length == 0 || mathang.Ten.Length > TenMaxLength)
            {
                return "Tên mặt hàng không hợp lệ";
            }

            LoaiHang[] DSloaihang = XL_LoaiHang.DocDanhSach("");
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

            if (!int.TryParse(sGia, out mathang.Gia))
            {
                return "Giá mặt hàng không hợp lệ";
            }
            if (mathang.Gia == 0 || mathang.Gia > GiaMaxValue)
            {
                return "Giá mặt hàng không hợp lệ";
            }

            string sInfo = LT_MatHang.Sua(mathangOld, mathang);
            if (string.IsNullOrEmpty(sInfo))
            {
                XL_Kho.CapNhatDS(mathangOld, mathang);
                XL_DonNhap.CapNhatDS(mathangOld, mathang);
                XL_DonXuat.CapNhatDS(mathangOld, mathang);
                mathangOld = mathang;
            }

            return sInfo;
        }

        public static string Xoa(string sMaSo, string sTen, string sLoaiHang, string sThuongHieu, string sGia)
        {
            MatHang mathang;
            mathang.MaSo = sMaSo;
            mathang.Ten = sTen;
            mathang.LoaiHang = sLoaiHang;
            mathang.ThuongHieu = sThuongHieu;

            if (mathang.MaSo.Length == 0 || mathang.MaSo.Length > MaSoMaxLength)
            {
                return "Mã mặt hàng không hợp lệ";
            }

            if (mathang.Ten.Length == 0 || mathang.Ten.Length > TenMaxLength)
            {
                return "Tên mặt hàng không hợp lệ";
            }

            LoaiHang[] DSloaihang = XL_LoaiHang.DocDanhSach("");
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

            if (!int.TryParse(sGia, out mathang.Gia))
            {
                return "Giá mặt hàng không hợp lệ";
            }
            if (mathang.Gia == 0 || mathang.Gia > GiaMaxValue)
            {
                return "Giá mặt hàng không hợp lệ";
            }

            if (string.IsNullOrEmpty(XL_DonNhap.MatHangTonTai(mathang)))
            {
                return "Không thể xóa mặt hàng được sử dụng trong đơn nhập";
            }

            if (string.IsNullOrEmpty(XL_DonXuat.MatHangTonTai(mathang)))
            {
                return "Không thể xóa mặt hàng được sử dụng trong đơn xuất";
            }

            return LT_MatHang.Xoa(mathang);
        }

        public static MatHang? ReadInfo(string sMatHangMaSo)
        {
            return LT_MatHang.ReadInfo(sMatHangMaSo);
        }
    }
}

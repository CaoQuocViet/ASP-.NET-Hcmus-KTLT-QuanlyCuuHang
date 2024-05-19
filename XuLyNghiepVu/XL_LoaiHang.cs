using CaoQuocViet.XuLyNghiepVu;
using CaoQuocViet.XuLyLuuTru;
using CaoQuocViet.Entities;

namespace CaoQuocViet.XuLyNghiepVu
{
    public class XL_LoaiHang
    {
        public const int MaSoMaxLength = 30;
        public const int TenMaxLength = 30;

        public static LoaiHang Empty()
        {
            return new LoaiHang() { MaSo = "", Ten = "" };
        }

        public static LoaiHang[] DocDanhSach(string sKeyword)
        {
            return LT_LoaiHang.DocDanhSach(sKeyword);
        }

        public static string Them(LoaiHang loaihang)
        {
            if (loaihang.MaSo.Length > MaSoMaxLength)
            {
                return "Mã loại hàng không hợp lệ";
            }
            if (loaihang.Ten.Length > TenMaxLength)
            {
                return "Tên loại hàng không hợp lệ";
            }

            return LT_LoaiHang.Them(loaihang);
        }

        public static string Sua(string sMaSo, string sTen, ref LoaiHang loaihangOld)
        {
            LoaiHang loaihang;
            loaihang.MaSo = sMaSo;
            loaihang.Ten = sTen;

            if (loaihang.MaSo.Length == 0 || loaihang.MaSo.Length > MaSoMaxLength)
            {
                return "Mã loại hàng không hợp lệ";
            }

            if (loaihang.Ten.Length == 0 || loaihang.Ten.Length > TenMaxLength)
            {
                return "Tên loại hàng không hợp lệ";
            }

            string sInfo = LT_LoaiHang.Sua(loaihangOld, loaihang);
            if (string.IsNullOrEmpty(sInfo))
            {
                XL_MatHang.CapNhatLoaiHang(loaihangOld, loaihang);
                loaihangOld = loaihang;
            }

            return sInfo;
        }

        public static string Xoa(string sMaSo, string sTen)
        {
            LoaiHang loaihang;
            loaihang.MaSo = sMaSo;
            loaihang.Ten = sTen;

            if (loaihang.MaSo.Length == 0 || loaihang.MaSo.Length > MaSoMaxLength)
            {
                return "Mã loại hàng không hợp lệ";
            }

            if (loaihang.Ten.Length == 0 || loaihang.Ten.Length > TenMaxLength)
            {
                return "Tên loại hàng không hợp lệ";
            }

            MatHang[] DSmathang = XL_MatHang.DocDanhSach("");
            for (int i = 0; i < DSmathang.Length; i++)
            {
                if (loaihang.Ten == DSmathang[i].LoaiHang)
                {
                    if (string.IsNullOrEmpty(XL_DonNhap.MatHangTonTai(DSmathang[i])))
                    {
                        return "Không thể xóa loại hàng có mặt hàng được sử dụng trong đơn nhập";
                    }

                    if (string.IsNullOrEmpty(XL_DonXuat.MatHangTonTai(DSmathang[i])))
                    {
                        return "Không thể xóa loại hàng có mặt hàng được sử dụng trong đơn xuất";
                    }
                }
            }

            return LT_LoaiHang.Xoa(loaihang);
        }

        public static LoaiHang? ReadInfo(string loaihangCode)
        {
            return LT_LoaiHang.ReadInfo(loaihangCode);
        }
    }
}

using Repo;
using Entities;
using System.Collections.Generic;

namespace Service
{
    public class XL_LoaiHang : IXL_LoaiHang
    {
        private readonly ILT_LoaiHang _luuTruLoaiHang = new LT_LoaiHang();
        private const int _maSoMaxLength = 30;
        // Tạo một đối tượng LoaiHang rỗng
        public LoaiHang Empty()
        {
            return new LoaiHang("", "");
        }

        // Đọc danh sách LoaiHang dựa trên từ khóa tìm kiếm
        public List<LoaiHang> DocDanhSach(string sKeyword)
        {
            return _luuTruLoaiHang.DocDanhSach(sKeyword);
        }

        // Thêm một đối tượng LoaiHang mới
        private const int _tenMaxLength = 100;

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

            return _luuTruLoaiHang.Them(loaihang);
        }

        // Sửa thông tin một đối tượng LoaiHang
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

        // Xóa một đối tượng LoaiHang
        public string Xoa(string sMaSo, string sTen)
        {
            LoaiHang loaihang = new LoaiHang(sMaSo, sTen);

            XL_MatHang xlMatHang = new XL_MatHang();
            List<MatHang> DSmathang = xlMatHang.DocDanhSach(sTen);
            for (int i = 0; i < DSmathang.Count; i++)
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

        // Đọc thông tin một đối tượng LoaiHang dựa trên mã loại hàng
        public LoaiHang DocThongTin(string loaihangCode)
        {
            return _luuTruLoaiHang.DocThongTin(loaihangCode);
        }
    }
}

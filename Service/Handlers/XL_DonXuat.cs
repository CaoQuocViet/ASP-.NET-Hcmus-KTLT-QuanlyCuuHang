using Repo;
using Entities;
using System.Collections.Generic;

namespace Service
{
    public class XL_DonXuat : IXL_DonXuat
    {
        private readonly ILT_DonXuat luuTruDonXuat = new LT_DonXuat();

        // Đọc danh sách Đơn xuất dựa trên từ khóa tìm kiếm
        public List<DonXuat> DocDanhSach(string sKeyword)
        {
            return luuTruDonXuat.DocDanhSach(sKeyword);
        }

        // Cập nhật danh sách Mặt hàng trong Đơn xuất
        public void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
        {
            luuTruDonXuat.CapNhatDS(mathangOld, mathangNew);
        }

        // Kiểm tra xem Mặt hàng đã tồn tại trong danh sách Đơn xuất chưa
        public string MatHangTonTai(MatHang mathang)
        {
            List<DonXuat> DSdonxuat = DocDanhSach("");
            for (int i = 0; i < DSdonxuat.Count; i++)
            {
                for (int k = 0; k < DSdonxuat[i].Kho.Count; k++)
                {
                    if (mathang.Ten == DSdonxuat[i].Kho[k].TenMatHang)
                    {
                        return string.Empty;
                    }
                }
            }

            return "Mặt hàng không tồn tại";
        }

        // Thêm Đơn xuất mới
        public string Them(string sMaSo, string sNgay, List<Kho> DSkho, ref DonXuat donxuat)
        {
            donxuat.MaSo = sMaSo;
            int MaSoMaxLength = 10;

            donxuat.Kho = DSkho;

            if (donxuat.MaSo.Length == 0 || donxuat.MaSo.Length > MaSoMaxLength)
            {
                return "Mã số Đơn xuất không hợp lệ";
            }

            DateOnly tempNgay;
            if (!DateOnly.TryParse(sNgay, out tempNgay))
            {
                return "Ngày Đơn xuất không hợp lệ";
            }
            donxuat.Ngay = tempNgay;

            string sInfo = luuTruDonXuat.Them(donxuat);
            if (string.IsNullOrEmpty(sInfo))
            {
                luuTruDonXuat.Them(donxuat);
            }

            return string.Empty;
        }

        // Sửa thông tin Đơn xuất
        public string Sua(string sMaSo, string sNgay, ref DonXuat donxuatOld)
        {
            DonXuat donxuat = new DonXuat(sMaSo, default, new List<Kho>());
            int MaSoMaxLength = 10;

            if (donxuat.MaSo.Length == 0 || donxuat.MaSo.Length > MaSoMaxLength)
            {
                return "Mã số Đơn xuất không hợp lệ";
            }

            DateOnly tempNgay;
            if (!DateOnly.TryParse(sNgay, out tempNgay))
            {
                return "Ngày Đơn xuất không hợp lệ";
            }
            donxuat.Ngay = tempNgay;

            string sInfo = luuTruDonXuat.Sua(donxuatOld, donxuat);
            if (string.IsNullOrEmpty(sInfo))
            {
                donxuatOld = donxuat;
            }

            return sInfo;
        }

        // Đọc thông tin Đơn xuất dựa trên mã số
        public DonXuat? ReadInfo(string donxuatMaSo)
        {
            return luuTruDonXuat.ReadInfo(donxuatMaSo);
        }
    }
}

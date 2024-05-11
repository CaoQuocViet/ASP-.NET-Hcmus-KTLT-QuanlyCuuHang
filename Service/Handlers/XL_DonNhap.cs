using Repo;
using Entities;
using Newtonsoft.Json;
using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;

namespace Service
{
    public class XL_DonNhap : IXL_DonNhap
    {
        private readonly ILT_DonNhap _luuTruDonNhap = new LT_DonNhap();

        // Hàm để đọc danh sách Đơn nhập dựa trên từ khóa tìm kiếm
        public List<DonNhap> DocDanhSach(string sKeyword)
        {
            return _luuTruDonNhap.DocDanhSach(sKeyword);
        }

        // Hàm để cập nhật danh sách Mặt hàng trong Đơn nhập
        public void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
        {
            _luuTruDonNhap.CapNhatDS(mathangOld, mathangNew);
        }

        // Hàm để kiểm tra xem Mặt hàng đã tồn tại trong Đơn nhập hay chưa
        public string MatHangTonTai(MatHang mathang)
        {
            List<DonNhap> DSdonnhap = DocDanhSach("");
            for (int i = 0; i < DSdonnhap.Count; i++)
            {
                for (int k = 0; k < DSdonnhap[i].Kho.Count; k++)
                {
                    if (mathang.Ten == DSdonnhap[i].Kho[k].TenMatHang)
                    {
                        return string.Empty;
                    }
                }
            }

            return "Mặt hàng không tồn tại";
        }

        // Hàm để thêm Đơn nhập mới
        public string Them(string sMaSo, string sNgay, List<Kho> DSkho, ref DonNhap donnhap)
        {
            donnhap.MaSo = sMaSo;
            int MaSoMaxLength = 10;
            donnhap.Kho = DSkho;

            if (donnhap.MaSo.Length == 0 || donnhap.MaSo.Length > MaSoMaxLength)
            {
                return "Mã số Đơn nhập không hợp lệ";
            }

            DateOnly parsedDate;
            if (!DateOnly.TryParse(sNgay, out parsedDate))
            {
                return "Ngày Đơn nhập không hợp lệ";
            }

            donnhap.Ngay = parsedDate;

            string sInfo = _luuTruDonNhap.Them(donnhap);
            if (string.IsNullOrEmpty(sInfo))
            {
                XL_Kho kho = new XL_Kho();
                kho.DonNhap(donnhap);
            }

            return string.Empty;
        }

        // Hàm để sửa thông tin Đơn nhập
        public string Sua(string sMaSo, string sNgay, ref DonNhap donnhapOld)
        {
            DateOnly parsedDate;
            if (!DateOnly.TryParse(sNgay, out parsedDate))
            {
                return "Ngày Đơn nhập không hợp lệ";
            }

            DonNhap donnhap = new DonNhap(sMaSo, parsedDate, new List<Kho>());
            int MaSoMaxLength = 10;

            if (donnhap.MaSo.Length == 0 || donnhap.MaSo.Length > MaSoMaxLength)
            {
                return "Mã số Đơn nhập không hợp lệ";
            }

            donnhap.Ngay = parsedDate;

            string sInfo = _luuTruDonNhap.Sua(donnhapOld, donnhap);
            if (string.IsNullOrEmpty(sInfo))
            {
                donnhapOld = donnhap;
            }

            return sInfo;
        }

        // Hàm để đọc thông tin Đơn nhập dựa trên mã số Đơn nhập
        public DonNhap? DocThongTin(string donnhapMaSo)
        {
            return _luuTruDonNhap.DocThongTin(donnhapMaSo);
        }
    }
}

using Repo;
using Entities;
using Newtonsoft.Json;
using System;
using System.Net.NetworkInformation;

namespace Service
{
    public class XL_DonNhap : IXL_DonNhap
    {
        private ILT_DonNhap _luuTruDonNhap = new LT_DonNhap();
        public DonNhap[] DocDanhSach(string sKeyword)
        {
            return _luuTruDonNhap.DocDanhSach(sKeyword);
        }

        public void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
        {
            _luuTruDonNhap.CapNhatDS(mathangOld, mathangNew);
        }

        public string MatHangTonTai(MatHang mathang)
        {
            DonNhap[] DSdonnhap = DocDanhSach("");
            for (int i = 0; i < DSdonnhap.Length; i++)
            {
                for (int k = 0; k < DSdonnhap[i].Kho.Length; k++)
                {
                    if (mathang.Ten == DSdonnhap[i].Kho[k].TenMatHang)
                    {
                        return string.Empty;
                    }
                }
            }

            return "Mặt hàng không tồn tại";
        }

        public string Them(string sMaSo, string sNgay, Kho[] DSkho, ref DonNhap donnhap)
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

        public string Sua(string sMaSo, string sNgay, ref DonNhap donnhapOld)
        {
            DonNhap donnhap = new DonNhap();
            int MaSoMaxLength = 10; 
            donnhap.MaSo = sMaSo;

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

            string sInfo = _luuTruDonNhap.Sua(donnhapOld, donnhap);  
            if (string.IsNullOrEmpty(sInfo))
            {
                donnhapOld = donnhap;
            }

            return sInfo;
        }

        public DonNhap? ReadInfo(string donnhapMaSo)
        {
            return _luuTruDonNhap.ReadInfo(donnhapMaSo);  
        }
    }
}

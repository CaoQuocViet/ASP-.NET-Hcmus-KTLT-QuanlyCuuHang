using Repo;
using Entities;
using System;

namespace Service
{
    public class XL_DonXuat : IXL_DonXuat
    {
        private ILT_DonXuat luuTruDonXuat = new LT_DonXuat();
        public DonXuat[] DocDanhSach(string sKeyword)
        {
            return luuTruDonXuat.DocDanhSach(sKeyword);
        }

        public void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
        {
            luuTruDonXuat.CapNhatDS(mathangOld, mathangNew);
        }

        public string MatHangTonTai(MatHang mathang)
        {
            DonXuat[] DSdonxuat = DocDanhSach("");
            for (int i = 0; i < DSdonxuat.Length; i++)
            {
                for (int k = 0; k < DSdonxuat[i].Kho.Length; k++)
                {
                    if (mathang.Ten == DSdonxuat[i].Kho[k].TenMatHang)
                    {
                        return string.Empty;
                    }
                }
            }

            return "Mặt hàng không tồn tại";
        }

        public string Them(string sMaSo, string sNgay, Kho[] DSkho, ref DonXuat donxuat)
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

        public string Sua(string sMaSo, string sNgay, ref DonXuat donxuatOld)
        {
            DonXuat donxuat = new DonXuat(sMaSo, default, new Kho[0]);
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

        public DonXuat? ReadInfo(string donxuatMaSo)
        {
            return luuTruDonXuat.ReadInfo(donxuatMaSo);
        }
    }
}

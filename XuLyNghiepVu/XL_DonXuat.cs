using CaoQuocViet.XuLyLuuTru;
using CaoQuocViet.Entities;
using System;

namespace CaoQuocViet.XuLyNghiepVu
{
    public class XL_DonXuat
    {
        public const int MaSoMaxLength = 30;

        public static DonXuat[] DocDanhSach(string sKeyword)
        {
            return LT_DonXuat.DocDanhSach(sKeyword);
        }

        public static void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
        {
            LT_DonXuat.CapNhatDS(mathangOld, mathangNew);
        }

        public static string MatHangTonTai(MatHang mathang)
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

        public static string Them(string sMaSo, string sNgay, Kho[] DSkho, ref DonXuat donxuat)
        {
            donxuat.MaSo = sMaSo;
            donxuat.Kho = DSkho;

            if (donxuat.MaSo.Length == 0 || donxuat.MaSo.Length > MaSoMaxLength)
            {
                return "Mã số Đơn xuất không hợp lệ";
            }

            if (!DateOnly.TryParse(sNgay, out donxuat.Ngay))
            {
                return "Ngày Đơn xuất không hợp lệ";
            }

            string sInfo = LT_DonXuat.Them(donxuat);
            if (string.IsNullOrEmpty(sInfo))
            {
                XL_Kho.DonXuat(donxuat);
            }

            return string.Empty;
        }

        public static string Sua(string sMaSo, string sNgay, ref DonXuat donxuatOld)
        {
            DonXuat donxuat = new DonXuat();
            donxuat.MaSo = sMaSo;

            if (donxuat.MaSo.Length == 0 || donxuat.MaSo.Length > MaSoMaxLength)
            {
                return "Mã số Đơn xuất không hợp lệ";
            }

            if (!DateOnly.TryParse(sNgay, out donxuat.Ngay))
            {
                return "Ngày Đơn xuất không hợp lệ";
            }

            string sInfo = LT_DonXuat.Sua(donxuatOld, donxuat);
            if (string.IsNullOrEmpty(sInfo))
            {
                donxuatOld = donxuat;
            }

            return sInfo;
        }

        public static string Xoa(DonXuat donxuat)
        {
            return LT_DonXuat.Xoa(donxuat);
        }

        public static DonXuat? ReadInfo(string donxuatMaSo)
        {
            return LT_DonXuat.ReadInfo(donxuatMaSo);
        }
    }
}

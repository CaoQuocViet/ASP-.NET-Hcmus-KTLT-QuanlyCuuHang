using CaoQuocViet.XuLyLuuTru;
using CaoQuocViet.Entities;
using Newtonsoft.Json;
using System;
using System.Net.NetworkInformation;

namespace CaoQuocViet.XuLyNghiepVu
{
    public class XL_DonNhap
    {
        public const int MaSoMaxLength = 30;

        public static DonNhap[] DocDanhSach(string sKeyword)
        {
            return LT_DonNhap.DocDanhSach(sKeyword);
        }

        public static void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
        {
            LT_DonNhap.CapNhatDS(mathangOld, mathangNew);
        }

        public static string MatHangTonTai(MatHang mathang)
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

        public static string Them(string sMaSo, string sNgay, Kho[] DSkho, ref DonNhap donnhap)
        {
            donnhap.MaSo = sMaSo;
            donnhap.Kho = DSkho;

            if (donnhap.MaSo.Length == 0 || donnhap.MaSo.Length > MaSoMaxLength)
            {
                return "Mã số Đơn nhập không hợp lệ";
            }

            if (!DateOnly.TryParse(sNgay, out donnhap.Ngay))
            {
                return "Ngày Đơn nhập không hợp lệ";
            }

            string sInfo = LT_DonNhap.Them(donnhap);
            if (string.IsNullOrEmpty(sInfo))
            {
                XL_Kho.DonNhap(donnhap);
            }

            return string.Empty;
        }

        public static string Sua(string sMaSo, string sNgay, ref DonNhap donnhapOld)
        {
            DonNhap donnhap = new DonNhap();
            donnhap.MaSo = sMaSo;

            if (donnhap.MaSo.Length == 0 || donnhap.MaSo.Length > MaSoMaxLength)
            {
                return "Mã số Đơn nhập không hợp lệ";
            }

            if (!DateOnly.TryParse(sNgay, out donnhap.Ngay))
            {
                return "Ngày Đơn nhập không hợp lệ";
            }

            string sInfo = LT_DonNhap.Sua(donnhapOld, donnhap);
            if (string.IsNullOrEmpty(sInfo))
            {
                donnhapOld = donnhap;
            }

            return sInfo;
        }

        public static string Xoa(DonNhap donnhap)
        {
            return LT_DonNhap.Xoa(donnhap);
        }

        public static DonNhap? ReadInfo(string donnhapMaSo)
        {
            return LT_DonNhap.ReadInfo(donnhapMaSo);
        }
    }
}

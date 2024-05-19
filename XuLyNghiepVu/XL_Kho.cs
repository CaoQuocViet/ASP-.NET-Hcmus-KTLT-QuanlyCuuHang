using CaoQuocViet.XuLyLuuTru;
using CaoQuocViet.Entities;
using Newtonsoft.Json;
using System;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace CaoQuocViet.XuLyNghiepVu
{
    public class XL_Kho
    {
        public const int MaSoMaxLength = 30;
        public const int UserMaxLength = 30;

        public static Kho[] DocDanhSach(string sKeyword, int Filter)
        {
            Kho[] DSkho = LT_Kho.DocDanhSach(sKeyword);

            if (1 == Filter)
            {
                DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);
                int count = 0;
                for (int i = 0; i < DSkho.Length; i++)
                {
                    if (0 > dateNow.CompareTo(DSkho[i].HanDung))
                    {
                        count++;
                    }
                }

                Kho[] unexpiryList = new Kho[count];
                count = 0;
                for (int i = 0; i < DSkho.Length; i++)
                {
                    if (0 > dateNow.CompareTo(DSkho[i].HanDung))
                    {
                        unexpiryList[count] = DSkho[i];
                        count++;
                    }
                }

                return unexpiryList;
            }

            if (2 == Filter)
            {
                DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);
                int count = 0;
                for (int i = 0; i < DSkho.Length; i++)
                {
                    if (0 < dateNow.CompareTo(DSkho[i].HanDung))
                    {
                        count++;
                    }
                }

                Kho[] expiryList = new Kho[count];
                count = 0;
                for (int i = 0; i < DSkho.Length; i++)
                {
                    if (0 < dateNow.CompareTo(DSkho[i].HanDung))
                    {
                        expiryList[count] = DSkho[i];
                        count++;
                    }
                }

                return expiryList;
            }

            return DSkho;
        }

        public static void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
        {
            LT_Kho.CapNhatDS(mathangOld, mathangNew);
        }

        public static string XacMinhNhapKho(string sTenMatHang, string sSoLuong, string sNgaySanXuat, string sHanDung, ref Kho kho)
        {
            kho.TenMatHang = sTenMatHang;
            MatHang[] DSmathang = XL_MatHang.DocDanhSach("");
            bool isValid = false;
            for (int i = 0; i < DSmathang.Length; i++)
            {
                if (kho.TenMatHang == DSmathang[i].Ten)
                {
                    isValid = true;
                    break;
                }
            }
            if (!isValid)
            {
                return "Mặt hàng không tồn tại";
            }

            if (!int.TryParse(sSoLuong, out kho.SoLuong) || kho.SoLuong == 0)
            {
                return "Số lượng không hợp lệ";
            }

            if (!DateOnly.TryParse(sNgaySanXuat, out kho.NgaySanXuat))
            {
                return "Ngày sản xuất không hợp lệ";
            }

            if (!DateOnly.TryParse(sHanDung, out kho.HanDung))
            {
                return "Ngày hết hạn không hợp lệ";
            }

            if (kho.NgaySanXuat.CompareTo(kho.HanDung) >= 0)
            {
                return "Ngày sản xuất phải trước ngày hết hạn";
            }

            return string.Empty;
        }

        public static string XacMinhXuatKho(string sTenMatHang, string sSoLuong, string sNgaySanXuat, string sHanDung, ref Kho kho)
        {
            kho.TenMatHang = sTenMatHang;
            MatHang[] DSmathang = XL_MatHang.DocDanhSach("");
            bool isValid = false;
            for (int i = 0; i < DSmathang.Length; i++)
            {
                if (kho.TenMatHang == DSmathang[i].Ten)
                {
                    isValid = true;
                    break;
                }
            }
            if (!isValid)
            {
                return "Mặt hàng không tồn tại";
            }

            if (!int.TryParse(sSoLuong, out kho.SoLuong) || kho.SoLuong == 0)
            {
                return "Số lượng không hợp lệ";
            }

            if (!DateOnly.TryParse(sNgaySanXuat, out kho.NgaySanXuat))
            {
                return "Ngày sản xuất không hợp lệ";
            }

            if (!DateOnly.TryParse(sHanDung, out kho.HanDung))
            {
                return "Ngày hết hạn không hợp lệ";
            }

            if (kho.NgaySanXuat.CompareTo(kho.HanDung) >= 0)
            {
                return "Ngày sản xuất phải trước ngày hết hạn";
            }

            Kho[] DSkho = DocDanhSach("", 0);

            isValid = false;
            for (int i = 0; i < DSkho.Length; i++)
            {
                if (kho.TenMatHang == DSkho[i].TenMatHang &&
                    kho.NgaySanXuat == DSkho[i].NgaySanXuat &&
                    kho.HanDung == DSkho[i].HanDung)
                {
                    isValid = true;
                    if (kho.SoLuong > DSkho[i].SoLuong)
                    {
                        return "Số lượng của mặt hàng không đủ";
                    }
                    break;
                }
            }

            if (!isValid)
            {
                return "Mặt hàng đã hết hàng";
            }

            return string.Empty;
        }

        public static string ThemVaoDS(Kho kho, ref Kho[] DSkho)
        {
            bool addFlag = false;
            for (int i = 0; i < DSkho.Length; i++)
            {
                if (kho.TenMatHang == DSkho[i].TenMatHang &&
                    kho.NgaySanXuat == DSkho[i].NgaySanXuat &&
                    kho.HanDung == DSkho[i].HanDung)
                {
                    DSkho[i].SoLuong += kho.SoLuong;
                    addFlag = true;
                }
            }

            if (!addFlag)
            {
                Kho[] DSkhoNew = new Kho[DSkho.Length + 1];
                for (int i = 0; i < DSkho.Length; i++)
                {
                    DSkhoNew[i] = DSkho[i];
                }
                DSkhoNew[DSkho.Length] = kho;
                DSkho = DSkhoNew;
            }

            return string.Empty;
        }

        public static void KiemTraHangHoaTonTai(string sKhoList, ref Kho[] DSkho)
        {
            StringReader reader = new StringReader(sKhoList);
            int count = 0;
            while (null != reader.ReadLine())
            {
                count++;
            }
            reader.Close();

            DSkho = new Kho[count];
            reader = new StringReader(sKhoList);
            for (int i = 0; i < DSkho.Length; i++)
            {
                string? sData = reader.ReadLine();
                if (null != sData)
                {
                    DSkho[i] = JsonConvert.DeserializeObject<Kho>(sData);
                }
            }
            reader.Close();
        }

        public static string TaoDanhSachHangHoa(Kho[] DSkho)
        {
            StringWriter writer = new StringWriter();
            foreach (Kho r in DSkho)
            {
                string sData = JsonConvert.SerializeObject(r);
                writer.WriteLine(sData);
            }
            string sResult = writer.ToString();
            writer.Close();

            return sResult;
        }

        public static void DonNhap(DonNhap donnhap)
        {
            LT_Kho.DonNhap(donnhap);
        }

        public static void DonXuat(DonXuat donxuat)
        {
            LT_Kho.DonXuat(donxuat);
        }
    }
}

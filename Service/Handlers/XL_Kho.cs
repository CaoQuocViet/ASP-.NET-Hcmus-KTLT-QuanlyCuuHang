using Repo;
using Entities;
using Newtonsoft.Json;
using System;

namespace Service
{
    public class XL_Kho : IXL_Kho
    {
        private ILT_Kho luuTruKho = new LT_Kho();

        public Kho[] DocDanhSach(string sKeyword, int Filter)
        {
            Kho[] DSkho = luuTruKho.DocDanhSach(sKeyword);

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

        public void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
        {
            luuTruKho.CapNhatDS(mathangOld, mathangNew);
        }

        public string XacMinhNhapKho(string sTenMatHang, string sSoLuong, string sNgaySanXuat, string sHanDung, ref Kho kho)
        {
            kho.TenMatHang = sTenMatHang;
            XL_MatHang xlMatHang = new XL_MatHang();  
            MatHang[] DSmathang = xlMatHang.DocDanhSach("");  
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

        public string XacMinhXuatKho(string sTenMatHang, string sSoLuong, string sNgaySanXuat, string sHanDung, ref Kho kho)
        {
            kho.TenMatHang = sTenMatHang;
            XL_MatHang xlMatHang = new XL_MatHang();  
            MatHang[] DSmathang = xlMatHang.DocDanhSach("");  
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

        public string ThemVaoDS(Kho kho, ref Kho[] DSkho)
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

        public void KiemTraHangHoaTonTai(string sKhoList, ref Kho[] DSkho)
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
                if (sData != null)
                {
                    Kho? deserializedKho = JsonConvert.DeserializeObject<Kho>(sData);
                    if (deserializedKho != null)
                    {
                        DSkho[i] = deserializedKho;
                    }
                }
            }
            reader.Close();
        }

        public string TaoDanhSachHangHoa(Kho[] DSkho)
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

        public void DonNhap(DonNhap donnhap)
        {
            LT_Kho lt_kho = new LT_Kho();
            lt_kho.DonNhap(donnhap);
        }

        public void DonXuat(DonXuat donxuat)
        {
            LT_Kho lt_kho = new LT_Kho();
            lt_kho.DonXuat(donxuat);
        }
    }
}

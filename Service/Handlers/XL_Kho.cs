using Repo.Repo;
using Repo;
using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Service
{
    public class XL_Kho : IXL_Kho
    {
        private ILT_Kho _luuTruKho = new LT_Kho();

        // Hàm để đọc danh sách kho dựa trên từ khóa và bộ lọc
        public List<Kho> DocDanhSach(string sKeyword, int Filter)
        {
            List<Kho> DSkho = _luuTruKho.DocDanhSach(sKeyword);

            // Lọc danh sách kho theo hạn dùng chưa hết
            if (1 == Filter)
            {
                // Lấy ngày hiện tại
                DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);
                int count = 0;
                for (int i = 0; i < DSkho.Count; i++)
                {
                    if (0 > dateNow.CompareTo(DSkho[i].HanDung))
                    {
                        count++;
                    }
                }

                List<Kho> unexpiryList = new List<Kho>(count);
                count = 0;
                for (int i = 0; i < DSkho.Count; i++)
                {
                    if (0 > dateNow.CompareTo(DSkho[i].HanDung))
                    {
                        unexpiryList.Add(DSkho[i]);
                        count++;
                    }
                }

                return unexpiryList;
            }

            // Lọc danh sách kho theo hạn dùng đã hết
            if (2 == Filter)
            {
                // Lấy ngày hiện tại
                DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);
                int count = 0;
                for (int i = 0; i < DSkho.Count; i++)
                {
                    if (0 < dateNow.CompareTo(DSkho[i].HanDung))
                    {
                        count++;
                    }
                }

                List<Kho> expiryList = new List<Kho>(count);
                count = 0;
                for (int i = 0; i < DSkho.Count; i++)
                {
                    if (0 < dateNow.CompareTo(DSkho[i].HanDung))
                    {
                        expiryList.Add(DSkho[i]);
                        count++;
                    }
                }

                return expiryList;
            }

            return DSkho;
        }

        // Hàm để cập nhật danh sách kho
        public void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
        {
            _luuTruKho.CapNhatDS(mathangOld, mathangNew);
        }

        // Hàm để xác minh thông tin nhập kho
        public string XacMinhNhapKho(string sTenMatHang, string sSoLuong, string sNgaySanXuat, string sHanDung, ref Kho kho)
        {
            kho.TenMatHang = sTenMatHang;
            XL_MatHang xlMatHang = new XL_MatHang();
            List<MatHang> DSmathang = xlMatHang.DocDanhSach("");
            bool isValid = false;
            for (int i = 0; i < DSmathang.Count; i++)
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

            int tempSoLuong;
            if (!int.TryParse(sSoLuong, out tempSoLuong) || tempSoLuong == 0)
            {
                return "Số lượng không hợp lệ";
            }
            kho.SoLuong = tempSoLuong;

            DateOnly tempNgaySanXuat;
            if (!DateOnly.TryParse(sNgaySanXuat, out tempNgaySanXuat))
            {
                return "Ngày sản xuất không hợp lệ";
            }
            kho.NgaySanXuat = tempNgaySanXuat;

            DateOnly tempHanDung;
            if (!DateOnly.TryParse(sHanDung, out tempHanDung))
            {
                return "Ngày hết hạn không hợp lệ";
            }
            kho.HanDung = tempHanDung;

            if (kho.NgaySanXuat.CompareTo(kho.HanDung) >= 0)
            {
                return "Ngày sản xuất phải trước ngày hết hạn";
            }

            return string.Empty;
        }

        // Hàm để xác minh thông tin xuất kho
        public string XacMinhXuatKho(string sTenMatHang, string sSoLuong, string sNgaySanXuat, string sHanDung, ref Kho kho)
        {
            kho.TenMatHang = sTenMatHang;
            XL_MatHang xlMatHang = new XL_MatHang();
            List<MatHang> DSmathang = xlMatHang.DocDanhSach("");
            bool isValid = false;
            for (int i = 0; i < DSmathang.Count; i++)
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

            int tempSoLuong;
            if (!int.TryParse(sSoLuong, out tempSoLuong) || tempSoLuong == 0)
            {
                return "Số lượng không hợp lệ";
            }
            kho.SoLuong = tempSoLuong;

            DateOnly tempNgaySanXuat;
            if (!DateOnly.TryParse(sNgaySanXuat, out tempNgaySanXuat))
            {
                return "Ngày sản xuất không hợp lệ";
            }
            kho.NgaySanXuat = tempNgaySanXuat;

            DateOnly tempHanDung;
            if (!DateOnly.TryParse(sHanDung, out tempHanDung))
            {
                return "Ngày hết hạn không hợp lệ";
            }
            kho.HanDung = tempHanDung;


            if (kho.NgaySanXuat.CompareTo(kho.HanDung) >= 0)
            {
                return "Ngày sản xuất phải trước ngày hết hạn";
            }

            List<Kho> DSkho = DocDanhSach("", 0);

            isValid = false;
            for (int i = 0; i < DSkho.Count; i++)
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

        // Hàm để thêm một kho vào danh sách kho
        public string ThemVaoDS(Kho kho, ref List<Kho> DSkho)
        {
            bool addFlag = false;
            for (int i = 0; i < DSkho.Count; i++)
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
                DSkho.Add(kho);
            }

            return string.Empty;
        }

        // Hàm để kiểm tra sự tồn tại của hàng hóa trong danh sách kho
        public void KiemTraHangHoaTonTai(string sKhoList, ref List<Kho> DSkho)
        {
            StringReader reader = new StringReader(sKhoList);
            int count = 0;
            while (null != reader.ReadLine())
            {
                count++;
            }
            reader.Close();

            DSkho = new List<Kho>(count);
            reader = new StringReader(sKhoList);
            for (int i = 0; i < DSkho.Count; i++)
            {
                string? sData = reader.ReadLine();
                if (sData != null)
                {
                    Kho? deserializedKho = JsonConvert.DeserializeObject<Kho>(sData);
                    if (deserializedKho != null)
                    {
                        DSkho.Add(deserializedKho);
                    }
                }
            }
            reader.Close();
        }

        // Hàm để tạo danh sách hàng hóa từ danh sách kho
        public string TaoDanhSachHangHoa(List<Kho> DSkho)
        {
            using StringWriter writer = new StringWriter();
            foreach (Kho r in DSkho)
            {
                string sData = JsonConvert.SerializeObject(r);
                writer.WriteLine(sData);
            }
            return writer.ToString();
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

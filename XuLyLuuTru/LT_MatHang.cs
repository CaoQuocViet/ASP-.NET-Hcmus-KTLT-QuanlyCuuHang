using CaoQuocViet.Entities;
using Newtonsoft.Json;

namespace CaoQuocViet.XuLyLuuTru
{
    public class LT_MatHang
    {
        public const string mathangFile = "Data/MatHang.json";

        public static MatHang[] DocDanhSach(string sKeyword)
        {
            StreamReader reader = new StreamReader(mathangFile);
            int count = 0;
            while (null != reader.ReadLine())
            {
                count++;
            }
            reader.Close();

            MatHang[] DSmathang = new MatHang[count];
            reader = new StreamReader(mathangFile);
            for (int i = 0; i < DSmathang.Length; i++)
            {
                string? sData = reader.ReadLine();
                if (null != sData)
                {
                    DSmathang[i] = JsonConvert.DeserializeObject<MatHang>(sData);
                }
            }
            reader.Close();

            if (string.IsNullOrEmpty(sKeyword))
            {
                return DSmathang;
            }

            count = 0;
            for (int i = 0; i < DSmathang.Length; i++)
            {
                if (DSmathang[i].MaSo.Contains(sKeyword) ||
                    DSmathang[i].Ten.Contains(sKeyword) ||
                    DSmathang[i].LoaiHang.Contains(sKeyword))
                {
                    count++;
                }
            }

            MatHang[] searchList = new MatHang[count];
            count = 0;
            for (int i = 0; i < DSmathang.Length; i++)
            {
                if (DSmathang[i].MaSo.Contains(sKeyword) ||
                    DSmathang[i].Ten.Contains(sKeyword) ||
                    DSmathang[i].LoaiHang.Contains(sKeyword))
                {
                    searchList[count++] = DSmathang[i];
                }
            }

            return searchList;
        }

        public static void LuuDanhSach(MatHang[] DSmathang)
        {
            StreamWriter writer = new StreamWriter(mathangFile);
            foreach (MatHang mathang in DSmathang)
            {
                string sData = JsonConvert.SerializeObject(mathang);
                writer.WriteLine(sData);
            }
            writer.Close();
        }

        public static string Them(MatHang mathang)
        {
            MatHang[] DSmathang = DocDanhSach("");

            for (int i = 0; i < DSmathang.Length; i++)
            {
                if (DSmathang[i].MaSo == mathang.MaSo)
                {
                    return "Mã mặt hàng đã tồn tại";
                }
                if (DSmathang[i].Ten == mathang.Ten)
                {
                    return "Tên mặt hàng đã tồn tại";
                }
            }

            MatHang[] DSmathangNew = new MatHang[DSmathang.Length + 1];
            DSmathangNew[0] = mathang;

            for (int i = 0; i < DSmathang.Length; i++)
            {
                DSmathangNew[i + 1] = DSmathang[i];
            }

            LuuDanhSach(DSmathangNew);

            return string.Empty;
        }

        public static string Sua(MatHang mathangOld, MatHang mathangNew)
        {
            MatHang[] DSmathang = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSmathang.Length; i++)
            {
                if (DSmathang[i].MaSo == mathangOld.MaSo || DSmathang[i].Ten == mathangOld.Ten)
                {
                    IsExist = true;
                    break;
                }
            }
            if (false == IsExist)
            {
                return "Mặt hàng không tồn tại";
            }

            for (int i = 0; i < DSmathang.Length; i++)
            {
                if (DSmathang[i].MaSo == mathangNew.MaSo && mathangOld.MaSo != mathangNew.MaSo)
                {
                    return "Mã mặt hàng mới đã tồn tại";
                }
                if (DSmathang[i].Ten == mathangNew.Ten && mathangOld.Ten != mathangNew.Ten)
                {
                    return "Tên mặt hàng mới đã tồn tại";
                }
            }

            for (int i = 0; i < DSmathang.Length; i++)
            {
                if (DSmathang[i].MaSo == mathangOld.MaSo || DSmathang[i].Ten == mathangOld.Ten)
                {
                    DSmathang[i] = mathangNew;
                    break;
                }
            }

            LuuDanhSach(DSmathang);

            return string.Empty;
        }

        public static string Xoa(MatHang mathang)
        {
            MatHang[] DSmathang = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSmathang.Length; i++)
            {
                if (DSmathang[i].MaSo == mathang.MaSo || DSmathang[i].Ten == mathang.Ten)
                {
                    IsExist = true;
                    break;
                }
            }
            if (false == IsExist)
            {
                return "Mặt hàng không tồn tại";
            }

            MatHang[] DSmathangNew = new MatHang[DSmathang.Length - 1];
            int count = 0;
            for (int i = 0; i < DSmathang.Length; i++)
            {
                if (DSmathang[i].MaSo != mathang.MaSo && DSmathang[i].Ten != mathang.Ten)
                {
                    DSmathangNew[count] = DSmathang[i];
                    count++;
                }
            }

            LuuDanhSach(DSmathangNew);

            return string.Empty;
        }

        public static MatHang? ReadInfo(string sMatHangMaSo)
        {
            MatHang[] DSmathang = DocDanhSach("");

            foreach (MatHang mathang in DSmathang) 
            {
                if (sMatHangMaSo == mathang.MaSo)
                {
                    return mathang;
                }
            }

            return null;
        }

        public static void CapNhatLoaiHang(LoaiHang loaihangOld, LoaiHang loaihangNew)
        {
            MatHang[] DSmathang = DocDanhSach("");

            for (int i = 0; i < DSmathang.Length; i++)
            {
                if (loaihangOld.Ten == DSmathang[i].LoaiHang)
                {
                    DSmathang[i].LoaiHang = loaihangNew.Ten;
                }
            }

            LuuDanhSach(DSmathang);
        }
    }
}

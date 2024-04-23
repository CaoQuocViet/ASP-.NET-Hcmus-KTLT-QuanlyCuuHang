using Entities;
using Newtonsoft.Json;

namespace Repo
{
    public class LT_MatHang : ILT_MatHang
    {
        private const string _mathangFile = "Data/MatHang.json";

        public MatHang[] DocDanhSach(string sKeyword)
        {
            StreamReader reader = new StreamReader(_mathangFile);
            int count = 0;
            while (null != reader.ReadLine())
            {
                count++;
            }
            reader.Close();

            MatHang[] DSmathang = new MatHang[count];
            reader = new StreamReader(_mathangFile);
            for (int i = 0; i < DSmathang.Length; i++)
            {
                string? sData = reader.ReadLine();
                if (sData != null)
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

        public void LuuDanhSach(MatHang[] DSmathang)
        {
            StreamWriter writer = new StreamWriter(_mathangFile);
            foreach (MatHang mathang in DSmathang)
            {
                string sData = JsonConvert.SerializeObject(mathang);
                writer.WriteLine(sData);
            }
            writer.Close();
        }

        public string Them(MatHang mathang)
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

        public string Sua(MatHang mathangOld, MatHang mathangNew)
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

        public string Xoa(MatHang mathang)
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

        public MatHang? ReadInfo(string sMatHangMaSo)
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

        public void CapNhatLoaiHang(LoaiHang loaihangOld, LoaiHang loaihangNew)
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

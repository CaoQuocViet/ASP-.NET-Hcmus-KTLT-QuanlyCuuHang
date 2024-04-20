using Entities;
using Newtonsoft.Json;

namespace Repo
{
    public class LT_LoaiHang : ILT_LoaiHang
    {
        private const string loaihangFile = "Data/LoaiHang.json";

        public LoaiHang[] DocDanhSach(string sKeyword)
        {
            StreamReader reader = new StreamReader(loaihangFile);
            int count = 0;
            while (null != reader.ReadLine())
            {
                count++;
            }
            reader.Close();

            LoaiHang[] DSloaihang = new LoaiHang[count];
            reader = new StreamReader(loaihangFile);
            for (int i = 0; i < DSloaihang.Length; i++)
            {
                string? sData = reader.ReadLine();
                if (null != sData)
                {
                    DSloaihang[i] = JsonConvert.DeserializeObject<LoaiHang>(sData);
                }
            }
            reader.Close();

            if (string.IsNullOrEmpty(sKeyword))
            {
                return DSloaihang;
            }

            count = 0;
            for (int i = 0; i < DSloaihang.Length; i++)
            {
                if (DSloaihang[i].MaSo.Contains(sKeyword) ||
                    DSloaihang[i].Ten.Contains(sKeyword))
                {
                    count++;
                }
            }

            LoaiHang[] searchList = new LoaiHang[count];
            count = 0;
            for (int i = 0; i < DSloaihang.Length; i++)
            {
                if (DSloaihang[i].MaSo.Contains(sKeyword) ||
                    DSloaihang[i].Ten.Contains(sKeyword))
                {
                    searchList[count++] = DSloaihang[i];
                }
            }

            return searchList;
        }

        public void LuuDanhSach(LoaiHang[] DSloaihang)
        {
            StreamWriter writer = new StreamWriter(loaihangFile);
            foreach (LoaiHang loaihang in DSloaihang)
            {
                string sData = JsonConvert.SerializeObject(loaihang);
                writer.WriteLine(sData);
            }
            writer.Close();
        }

        public string Them(LoaiHang loaihang)
        {
            LoaiHang[] DSloaihang = DocDanhSach("");

            for (int i = 0; i < DSloaihang.Length; i++)
            {
                if (DSloaihang[i].MaSo == loaihang.MaSo)
                {
                    return "Mã loại hàng đã tồn tại";
                }
                if (DSloaihang[i].Ten == loaihang.Ten)
                {
                    return "Tên loại hàng đã tồn tại";
                }
            }

            LoaiHang[] DSloaihangNew = new LoaiHang[DSloaihang.Length + 1];
            DSloaihangNew[0] = loaihang;

            for (int i = 0; i < DSloaihang.Length; i++)
            {
                DSloaihangNew[i + 1] = DSloaihang[i];
            }

            LuuDanhSach(DSloaihangNew);

            return string.Empty;
        }

        public string Sua(LoaiHang loaihangOld, LoaiHang loaihangNew)
        {
            LoaiHang[] DSloaihang = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSloaihang.Length; i++)
            {
                if (DSloaihang[i].MaSo == loaihangOld.MaSo || DSloaihang[i].Ten == loaihangOld.Ten)
                {
                    IsExist = true;
                    break;
                }
            }
            if (false == IsExist)
            {
                return "Loại hàng không tồn tại";
            }

            for (int i = 0; i < DSloaihang.Length; i++)
            {
                if (DSloaihang[i].MaSo == loaihangOld.MaSo || DSloaihang[i].Ten == loaihangOld.Ten)
                {
                    DSloaihang[i] = loaihangNew;
                    break;
                }
            }

            LuuDanhSach(DSloaihang);

            return string.Empty;
        }

        public string Xoa(LoaiHang loaihang)
        {
            LoaiHang[] DSloaihang = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSloaihang.Length; i++)
            {
                if (DSloaihang[i].MaSo == loaihang.MaSo || DSloaihang[i].Ten == loaihang.Ten)
                {
                    IsExist = true;
                    break;
                }
            }
            if (false == IsExist)
            {
                return "Loại hàng không tồn tại";
            }

            LoaiHang[] DSloaihangNew = new LoaiHang[DSloaihang.Length - 1];
            int count = 0;
            for (int i = 0; i < DSloaihang.Length; i++)
            {
                if (DSloaihang[i].MaSo != loaihang.MaSo && DSloaihang[i].Ten != loaihang.Ten)
                {
                    DSloaihangNew[count] = DSloaihang[i];
                    count++;
                }
            }

            LuuDanhSach(DSloaihangNew);

            return string.Empty;
        }

        public LoaiHang? ReadInfo(string loaihangCode)
        {
            LoaiHang[] DSloaihang = DocDanhSach("");

            foreach (LoaiHang loaihang in DSloaihang)
            {
                if (loaihangCode == loaihang.MaSo)
                {
                    return loaihang;
                }
            }

            return null;
        }
    }
}

using CaoQuocViet.Entities;
using Newtonsoft.Json;

namespace CaoQuocViet.XuLyLuuTru
{
    public class LT_DonNhap
    {
        public const string donnhapFile = "Data/DonNhap.json";

        public static DonNhap[] DocDanhSach(string sKeyword)
        {
            StreamReader reader = new StreamReader(donnhapFile);
            int count = 0;
            while (null != reader.ReadLine())
            {
                count++;
            }
            reader.Close();

            DonNhap[] DSdonnhap = new DonNhap[count];
            reader = new StreamReader(donnhapFile);
            for (int i = 0; i < DSdonnhap.Length; i++)
            {
                string? sData = reader.ReadLine();
                if (null != sData)
                {
                    DSdonnhap[i] = JsonConvert.DeserializeObject<DonNhap>(sData);
                }
            }
            reader.Close();

            if (string.IsNullOrEmpty(sKeyword))
            {
                return DSdonnhap;
            }
            
            count = 0;
            for (int i = 0; i < DSdonnhap.Length; i++)
            {
                if (DSdonnhap[i].MaSo.Contains(sKeyword))
                {
                    count++;
                }
            }

            DonNhap[] searchList = new DonNhap[count];
            count = 0;
            for (int i = 0; i < DSdonnhap.Length; i++)
            {
                if (DSdonnhap[i].MaSo.Contains(sKeyword))
                {
                    searchList[count++] = DSdonnhap[i];
                }
            }

            return searchList;
        }

        public static void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
        {
            DonNhap[] DSdonnhap = DocDanhSach("");

            for (int i = 0; i < DSdonnhap.Length; i++)
            {
                for (int k = 0; k < DSdonnhap[i].Kho.Length; k++)
                {
                    if (mathangOld.Ten == DSdonnhap[i].Kho[k].TenMatHang)
                    {
                        DSdonnhap[i].Kho[k].TenMatHang = mathangNew.Ten;
                    }
                }
            }

            LuuDanhSach(DSdonnhap);
        }

        public static void LuuDanhSach(DonNhap[] DSdonnhap)
        {
            StreamWriter writer = new StreamWriter(donnhapFile);
            foreach (DonNhap donnhap in DSdonnhap)
            {
                string sData = JsonConvert.SerializeObject(donnhap);
                writer.WriteLine(sData);
            }
            writer.Close();
        }

        public static string Them(DonNhap donnhap)
        {
            DonNhap[] DSdonnhap = DocDanhSach("");

            for (int i = 0; i < DSdonnhap.Length; i++)
            {
                if (DSdonnhap[i].MaSo == donnhap.MaSo)
                {
                    return "Đơn nhập đã tồn tại";
                }
            }

            DonNhap[] DSdonnhapNew = new DonNhap[DSdonnhap.Length + 1];
            DSdonnhapNew[0] = donnhap;

            for (int i = 0; i < DSdonnhap.Length; i++)
            {
                DSdonnhapNew[i + 1] = DSdonnhap[i];
            }

            LuuDanhSach(DSdonnhapNew);

            return string.Empty;
        }

        public static string Sua(DonNhap donnhapOld, DonNhap donnhapNew)
        {
            DonNhap[] DSdonnhap = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSdonnhap.Length; i++)
            {
                if (DSdonnhap[i].MaSo == donnhapOld.MaSo)
                {
                    IsExist = true;
                    break;
                }
            }
            if (false == IsExist)
            {
                return "Đơn nhập không tồn tại";
            }

            for (int i = 0; i < DSdonnhap.Length; i++)
            {
                if (DSdonnhap[i].MaSo == donnhapOld.MaSo)
                {
                    DSdonnhap[i].MaSo = donnhapNew.MaSo;
                    DSdonnhap[i].Ngay = donnhapNew.Ngay;
                    break;
                }
            }

            LuuDanhSach(DSdonnhap);

            return string.Empty;
        }

        public static string Xoa(DonNhap donnhap)
        {
            DonNhap[] DSdonnhap = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSdonnhap.Length; i++)
            {
                if (DSdonnhap[i].MaSo == donnhap.MaSo)
                {
                    IsExist = true;
                    break;
                }
            }
            if (false == IsExist)
            {
                return "Đơn nhập không tồn tại";
            }

            DonNhap[] DSdonnhapNew = new DonNhap[DSdonnhap.Length - 1];
            int count = 0;
            for (int i = 0; i < DSdonnhap.Length; i++)
            {
                if (DSdonnhap[i].MaSo != donnhap.MaSo)
                {
                    DSdonnhapNew[count] = DSdonnhap[i];
                    count++;
                }
            }

            LuuDanhSach(DSdonnhapNew);

            return string.Empty;
        }

        public static DonNhap? ReadInfo(string donnhapMaSo)
        {
            DonNhap[] DSdonnhap = DocDanhSach("");

            foreach (DonNhap donnhap in DSdonnhap)
            {
                if (donnhapMaSo == donnhap.MaSo)
                {
                    return donnhap;
                }
            }

            return null;
        }
    }
}

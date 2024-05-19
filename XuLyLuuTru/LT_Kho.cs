using CaoQuocViet.Entities;
using Newtonsoft.Json;

namespace CaoQuocViet.XuLyLuuTru
{
    public class LT_Kho
    {
        public const string khoFile = "Data/Kho.json";

        public static Kho[] DocDanhSach(string sKeyword)
        {
            StreamReader reader = new StreamReader(khoFile);
            int count = 0;
            while (null != reader.ReadLine())
            {
                count++;
            }
            reader.Close();

            Kho[] DSkho = new Kho[count];
            reader = new StreamReader(khoFile);
            for (int i = 0; i < DSkho.Length; i++)
            {
                string? sData = reader.ReadLine();
                if (null != sData)
                {
                    DSkho[i] = JsonConvert.DeserializeObject<Kho>(sData);
                }
            }
            reader.Close();

            if (string.IsNullOrEmpty(sKeyword))
            {
                return DSkho;
            }

            count = 0;
            for (int i = 0; i < DSkho.Length; i++)
            {
                if (DSkho[i].TenMatHang.Contains(sKeyword))
                {
                    count++;
                }
            }

            Kho[] searchList = new Kho[count];
            count = 0;
            for (int i = 0; i < DSkho.Length; i++)
            {
                if (DSkho[i].TenMatHang.Contains(sKeyword))
                {
                    searchList[count++] = DSkho[i];
                }
            }

            return searchList;
        }

        public static void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
        {
            Kho[] DSkho = DocDanhSach("");

            for (int i = 0; i < DSkho.Length; i++)
            {
                if (mathangOld.Ten == DSkho[i].TenMatHang)
                {
                    DSkho[i].TenMatHang = mathangNew.Ten;
                }
            }

            LuuDanhSach(DSkho);
        }

        public static void DonNhap(DonNhap donnhap)
        {
            Kho[] DSkho = DocDanhSach("");

            bool isExist = false;
            int count = 0;
            for (int i = 0; i < donnhap.Kho.Length; i++)
            {
                for (int k = 0; k < DSkho.Length; k++)
                {
                    if (donnhap.Kho[i].TenMatHang == DSkho[k].TenMatHang &&
                        donnhap.Kho[i].NgaySanXuat == DSkho[k].NgaySanXuat &&
                        donnhap.Kho[i].HanDung == DSkho[k].HanDung)
                    {
                        DSkho[k].SoLuong += donnhap.Kho[i].SoLuong;
                        isExist = true;
                        break;
                    }
                }

                if (!isExist)
                {
                    count++;
                }

                isExist = false;
            }

            if (count > 0)
            {
                Kho[] DSkhoNew = new Kho[DSkho.Length + count];
                for (int i = 0; i < count; i++)
                {
                    for (int k = 0; k < DSkho.Length; k++)
                    {
                        if (donnhap.Kho[i].TenMatHang == DSkho[k].TenMatHang &&
                            donnhap.Kho[i].NgaySanXuat == DSkho[k].NgaySanXuat &&
                            donnhap.Kho[i].HanDung == DSkho[k].HanDung)
                        {
                            isExist = true;
                            break;
                        }
                    }

                    if (!isExist)
                    {
                        DSkhoNew[i] = donnhap.Kho[i];
                    }

                    isExist = false;
                }

                for (int i = 0; i < DSkho.Length; i++)
                {
                    DSkhoNew[i + count] = DSkho[i];
                }

                DSkho = DSkhoNew;
            }

            LuuDanhSach(DSkho);
        }

        public static void DonXuat(DonXuat donxuat)
        {
            Kho[] DSkho = DocDanhSach("");

            for (int i = 0; i < donxuat.Kho.Length; i++)
            {
                for (int k = 0; k < DSkho.Length; k++)
                {
                    if (donxuat.Kho[i].TenMatHang == DSkho[k].TenMatHang &&
                        donxuat.Kho[i].NgaySanXuat == DSkho[k].NgaySanXuat &&
                        donxuat.Kho[i].HanDung == DSkho[k].HanDung)
                    {
                        DSkho[k].SoLuong -= donxuat.Kho[i].SoLuong;
                        break;
                    }
                }
            }

            int count = 0;
            for (int i = 0; i < DSkho.Length; i++)
            {
                if (DSkho[i].SoLuong != 0)
                {
                    count++;
                }
            }

            if (count > 0)
            {
                Kho[] DSkhoNew = new Kho[count];
                count = 0;
                for (int i = 0; i < DSkho.Length; i++)
                {
                    if (DSkho[i].SoLuong != 0)
                    {
                        DSkhoNew[count] = DSkho[i];
                        count++;
                    }
                }

                DSkho = DSkhoNew;
            }

            LuuDanhSach(DSkho);
        }

        public static void LuuDanhSach(Kho[] DSkho)
        {
            StreamWriter writer = new StreamWriter(khoFile);
            foreach (Kho kho in DSkho)
            {
                string sData = JsonConvert.SerializeObject(kho);
                writer.WriteLine(sData);
            }
            writer.Close();
        }
    }
}

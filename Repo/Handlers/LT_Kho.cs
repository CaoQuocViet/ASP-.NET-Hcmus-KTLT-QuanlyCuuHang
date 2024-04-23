using Entities;
using Newtonsoft.Json;

namespace Repo
{
    public class LT_Kho : ILT_Kho
    {
        private const string _khoFile = "Data/Kho.json";

        // Đọc danh sách các mặt hàng trong kho
        public Kho[] DocDanhSach(string sKeyword)
        {
            StreamReader reader = new StreamReader(_khoFile);
            int count = 0;
            while (null != reader.ReadLine())
            {
                count++;
            }
            reader.Close();

            Kho[] DSkho = new Kho[count];
            reader = new StreamReader(_khoFile);
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
                if (DSkho[i]?.TenMatHang?.Contains(sKeyword) == true)
                {
                    count++;
                }
            }

            Kho[] searchList = new Kho[count];
            count = 0;
            for (int i = 0; i < DSkho.Length; i++)
            {
                if (DSkho[i]?.TenMatHang?.Contains(sKeyword) == true)
                {
                    searchList[count++] = DSkho[i];
                }
            }

            return searchList;
        }

        // Cập nhật danh sách mặt hàng trong kho
        public void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
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

        // Thêm đơn nhập vào kho
        public void DonNhap(DonNhap donnhap)
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

        // Thêm đơn xuất khỏi kho
        public void DonXuat(DonXuat donxuat)
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

        // Lưu danh sách mặt hàng trong kho vào file
        public void LuuDanhSach(Kho[] DSkho)
        {
            StreamWriter writer = new StreamWriter(_khoFile);
            foreach (Kho kho in DSkho)
            {
                string sData = JsonConvert.SerializeObject(kho);
                writer.WriteLine(sData);
            }
            writer.Close();
        }
    }
}

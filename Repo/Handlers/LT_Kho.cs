using Entities;
using Newtonsoft.Json;

namespace Repo
{
    using Entities;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;

    namespace Repo
    {
        public class LT_Kho : ILT_Kho
        {
            private const string _khoFile = "Data/Kho.json";

            // Đọc danh sách các mặt hàng trong kho
            public List<Kho> DocDanhSach(string sKeyword)
            {
                List<Kho> DSkho = new List<Kho>();
                using (StreamReader reader = new StreamReader(_khoFile))
                {
                    string sData;
                    while ((sData = reader.ReadLine()) != null)
                    {
                        Kho kho = JsonConvert.DeserializeObject<Kho>(sData);
                        DSkho.Add(kho);
                    }
                }

                if (string.IsNullOrEmpty(sKeyword))
                {
                    return DSkho;
                }

                List<Kho> searchList = new List<Kho>();
                foreach (Kho kho in DSkho)
                {
                    if (kho.TenMatHang?.Contains(sKeyword) == true)
                    {
                        searchList.Add(kho);
                    }
                }

                return searchList;
            }

            // Cập nhật danh sách mặt hàng trong kho
            public void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
            {
                List<Kho> DSkho = DocDanhSach("");

                for (int i = 0; i < DSkho.Count; i++)
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
                List<Kho> DSkho = DocDanhSach("");

                bool isExist = false;
                int count = 0;
                foreach (Kho kho in donnhap.Kho)
                {
                    foreach (Kho existingKho in DSkho)
                    {
                        if (kho.TenMatHang == existingKho.TenMatHang &&
                            kho.NgaySanXuat == existingKho.NgaySanXuat &&
                            kho.HanDung == existingKho.HanDung)
                        {
                            existingKho.SoLuong += kho.SoLuong;
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
                    List<Kho> DSkhoNew = new List<Kho>(DSkho);
                    for (int i = 0; i < count; i++)
                    {
                        foreach (Kho kho in DSkho)
                        {
                            if (donnhap.Kho[i].TenMatHang == kho.TenMatHang &&
                                donnhap.Kho[i].NgaySanXuat == kho.NgaySanXuat &&
                                donnhap.Kho[i].HanDung == kho.HanDung)
                            {
                                isExist = true;
                                break;
                            }
                        }

                        if (!isExist)
                        {
                            DSkhoNew.Add(donnhap.Kho[i]);
                        }

                        isExist = false;
                    }

                    DSkho = DSkhoNew;
                }

                LuuDanhSach(DSkho);
            }

            // Thêm đơn xuất khỏi kho
            public void DonXuat(DonXuat donxuat)
            {
                List<Kho> DSkho = DocDanhSach("");

                foreach (Kho kho in donxuat.Kho)
                {
                    foreach (Kho existingKho in DSkho)
                    {
                        if (kho.TenMatHang == existingKho.TenMatHang &&
                            kho.NgaySanXuat == existingKho.NgaySanXuat &&
                            kho.HanDung == existingKho.HanDung)
                        {
                            existingKho.SoLuong -= kho.SoLuong;
                            break;
                        }
                    }
                }

                List<Kho> DSkhoNew = new List<Kho>();
                foreach (Kho kho in DSkho)
                {
                    if (kho.SoLuong != 0)
                    {
                        DSkhoNew.Add(kho);
                    }
                }

                LuuDanhSach(DSkhoNew);
            }

            // Lưu danh sách mặt hàng trong kho vào file
            public void LuuDanhSach(List<Kho> DSkho)
            {
                using (StreamWriter writer = new StreamWriter(_khoFile))
                {
                    foreach (Kho kho in DSkho)
                    {
                        string sData = JsonConvert.SerializeObject(kho);
                        writer.WriteLine(sData);
                    }
                }
            }
        }
    }
}

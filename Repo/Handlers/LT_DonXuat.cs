﻿using Entities;
using Newtonsoft.Json;

namespace Repo
{
    public class LT_DonXuat : ILT_DonXuat
    {
        private const string _donxuatFile = "Data/DonXuat.json";

        // Đọc danh sách Đơn Xuất từ file và tìm kiếm theo từ khóa
        public DonXuat[] DocDanhSach(string sKeyword)
        {
            StreamReader reader = new StreamReader(_donxuatFile);
            int count = 0;
            while (null != reader.ReadLine())
            {
                count++;
            }
            reader.Close();

            DonXuat[] DSdonxuat = new DonXuat[count];
            reader = new StreamReader(_donxuatFile);
            for (int i = 0; i < DSdonxuat.Length; i++)
            {
                string? sData = reader.ReadLine();
                if (null != sData)
                {
                    DSdonxuat[i] = JsonConvert.DeserializeObject<DonXuat>(sData);
                }
            }
            reader.Close();

            if (string.IsNullOrEmpty(sKeyword))
            {
                return DSdonxuat;
            }

            count = 0;
            for (int i = 0; i < DSdonxuat.Length; i++)
            {
                if (DSdonxuat[i].MaSo.Contains(sKeyword))
                {
                    count++;
                }
            }

            DonXuat[] searchList = new DonXuat[count];
            count = 0;
            for (int i = 0; i < DSdonxuat.Length; i++)
            {
                if (DSdonxuat[i].MaSo.Contains(sKeyword))
                {
                    searchList[count++] = DSdonxuat[i];
                }
            }

            return searchList;
        }

        // Cập nhật danh sách Đơn Xuất với thông tin mới
        public void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
        {
            DonXuat[] DSdonxuat = DocDanhSach("");

            for (int i = 0; i < DSdonxuat.Length; i++)
            {
                for (int k = 0; k < DSdonxuat[i].Kho.Length; k++)
                {
                    if (mathangOld.Ten == DSdonxuat[i].Kho[k].TenMatHang)
                    {
                        DSdonxuat[i].Kho[k].TenMatHang = mathangNew.Ten;
                    }
                }
            }

            LuuDanhSach(DSdonxuat);
        }

        // Lưu danh sách Đơn Xuất vào file
        public void LuuDanhSach(DonXuat[] DSdonxuat)
        {
            StreamWriter writer = new StreamWriter(_donxuatFile);
            foreach (DonXuat donxuat in DSdonxuat)
            {
                string sData = JsonConvert.SerializeObject(donxuat);
                writer.WriteLine(sData);
            }
            writer.Close();
        }

        // Thêm Đơn Xuất mới vào danh sách
        public string Them(DonXuat donxuat)
        {
            DonXuat[] DSdonxuat = DocDanhSach("");

            for (int i = 0; i < DSdonxuat.Length; i++)
            {
                if (DSdonxuat[i].MaSo == donxuat.MaSo)
                {
                    return "Mã số Đơn Xuất đã tồn tại";
                }
            }

            DonXuat[] DSdonxuatNew = new DonXuat[DSdonxuat.Length + 1];
            DSdonxuatNew[0] = donxuat;

            for (int i = 0; i < DSdonxuat.Length; i++)
            {
                DSdonxuatNew[i + 1] = DSdonxuat[i];
            }

            LuuDanhSach(DSdonxuatNew);

            return string.Empty;
        }

        // Sửa thông tin Đơn Xuất trong danh sách
        public string Sua(DonXuat donxuatOld, DonXuat donxuatNew)
        {
            DonXuat[] DSdonxuat = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSdonxuat.Length; i++)
            {
                if (DSdonxuat[i].MaSo == donxuatOld.MaSo)
                {
                    IsExist = true;
                    break;
                }
            }
            if (!IsExist)
            {
                return "Đơn Xuất không tồn tại";
            }

            for (int i = 0; i < DSdonxuat.Length; i++)
            {
                if (DSdonxuat[i].MaSo == donxuatOld.MaSo)
                {
                    DSdonxuat[i].MaSo = donxuatNew.MaSo;
                    DSdonxuat[i].Ngay = donxuatNew.Ngay;
                    break;
                }
            }

            LuuDanhSach(DSdonxuat);

            return string.Empty;
        }

        // Xóa Đơn Xuất khỏi danh sách
        public string Xoa(DonXuat donxuat)
        {
            DonXuat[] DSdonxuat = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSdonxuat.Length; i++)
            {
                if (DSdonxuat[i].MaSo == donxuat.MaSo)
                {
                    IsExist = true;
                    break;
                }
            }
            if (!IsExist)
            {
                return "Đơn Xuất không tồn tại";
            }

            DonXuat[] DSdonxuatNew = new DonXuat[DSdonxuat.Length - 1];
            int count = 0;
            for (int i = 0; i < DSdonxuat.Length; i++)
            {
                if (DSdonxuat[i].MaSo != donxuat.MaSo)
                {
                    DSdonxuatNew[count] = DSdonxuat[i];
                    count++;
                }
            }

            LuuDanhSach(DSdonxuatNew);

            return string.Empty;
        }

        // Đọc thông tin Đơn Xuất dựa trên mã số
        public DonXuat? ReadInfo(string donxuatCode)
        {
            DonXuat[] DSdonxuat = DocDanhSach("");

            foreach (DonXuat donxuat in DSdonxuat)
            {
                if (donxuatCode == donxuat.MaSo)
                {
                    return donxuat;
                }
            }

            return null;
        }
    }
}

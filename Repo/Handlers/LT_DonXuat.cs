using Entities;
using Newtonsoft.Json;

namespace Repo
{
    public class LT_DonXuat : ILT_DonXuat
    {
        private const string _donxuatFile = "Data/DonXuat.json";

        // Đọc danh sách Đơn Xuất từ file và tìm kiếm theo từ khóa
        public List<DonXuat> DocDanhSach(string sKeyword)
        {
            StreamReader reader = new StreamReader(_donxuatFile);
            List<DonXuat> DSdonxuat = new List<DonXuat>();
            string? sData;
            while ((sData = reader.ReadLine()) != null)
            {
                DSdonxuat.Add(JsonConvert.DeserializeObject<DonXuat>(sData));
            }
            reader.Close();

            if (string.IsNullOrEmpty(sKeyword))
            {
                return DSdonxuat;
            }

            List<DonXuat> searchList = new List<DonXuat>();
            foreach (DonXuat donxuat in DSdonxuat)
            {
                if (donxuat.MaSo.Contains(sKeyword))
                {
                    searchList.Add(donxuat);
                }
            }

            return searchList;
        }

        // Cập nhật danh sách Đơn Xuất với thông tin mới
        public void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
        {
            List<DonXuat> DSdonxuat = DocDanhSach("");

            foreach (DonXuat donxuat in DSdonxuat)
            {
                foreach (Kho kho in donxuat.Kho)
                {
                    if (mathangOld.Ten == kho.TenMatHang)
                    {
                        kho.TenMatHang = mathangNew.Ten;
                    }
                }
            }

            LuuDanhSach(DSdonxuat);
        }

        // Lưu danh sách Đơn Xuất vào file
        public void LuuDanhSach(List<DonXuat> DSdonxuat)
        {
            using (StreamWriter writer = new StreamWriter(_donxuatFile))
            {
                foreach (DonXuat donxuat in DSdonxuat)
                {
                    string sData = JsonConvert.SerializeObject(donxuat);
                    writer.WriteLine(sData);
                }
            }
        }

        // Thêm Đơn Xuất mới vào danh sách
        public string Them(DonXuat donxuat)
        {
            List<DonXuat> DSdonxuat = DocDanhSach("");

            for (int i = 0; i < DSdonxuat.Count; i++)
            {
                if (DSdonxuat[i].MaSo == donxuat.MaSo)
                {
                    return "Mã số Đơn Xuất đã tồn tại";
                }
            }

            DSdonxuat.Insert(0, donxuat);

            LuuDanhSach(DSdonxuat);

            return string.Empty;
        }

        // Sửa thông tin Đơn Xuất trong danh sách
        public string Sua(DonXuat donxuatOld, DonXuat donxuatNew)
        {
            List<DonXuat> DSdonxuat = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSdonxuat.Count; i++)
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

            for (int i = 0; i < DSdonxuat.Count; i++)
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
            List<DonXuat> DSdonxuat = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSdonxuat.Count; i++)
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

            DSdonxuat.RemoveAll(d => d.MaSo == donxuat.MaSo);

            LuuDanhSach(DSdonxuat);

            return string.Empty;
        }

        // Đọc thông tin Đơn Xuất dựa trên mã số
        public DonXuat? ReadInfo(string donxuatCode)
        {
            List<DonXuat> DSdonxuat = DocDanhSach("");

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

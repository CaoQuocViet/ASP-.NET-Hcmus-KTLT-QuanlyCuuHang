using Entities;
using Newtonsoft.Json;

namespace Repo
{
    public class LT_LoaiHang : ILT_LoaiHang
    {
        private const string _loaihangFile = "Data/LoaiHang.json";

        // Đọc danh sách loại hàng từ file và tìm kiếm theo từ khóa
        public List<LoaiHang> DocDanhSach(string sKeyword)
        {
            List<LoaiHang> DSloaihang = new List<LoaiHang>();
            using (StreamReader reader = new StreamReader(_loaihangFile))
            {
                string sData;
                while ((sData = reader.ReadLine()) != null)
                {
                    DSloaihang.Add(JsonConvert.DeserializeObject<LoaiHang>(sData));
                }
            }

            if (string.IsNullOrEmpty(sKeyword))
            {
                return DSloaihang;
            }

            List<LoaiHang> searchList = new List<LoaiHang>();
            foreach (LoaiHang loaihang in DSloaihang)
            {
                if (loaihang.MaSo?.Contains(sKeyword) == true || loaihang.Ten?.Contains(sKeyword) == true)
                {
                    searchList.Add(loaihang);
                }
            }

            return searchList;
        }

        // Lưu danh sách loại hàng vào file
        public void LuuDanhSach(List<LoaiHang> DSloaihang)
        {
            using (StreamWriter writer = new StreamWriter(_loaihangFile))
            {
                foreach (LoaiHang loaihang in DSloaihang)
                {
                    string sData = JsonConvert.SerializeObject(loaihang);
                    writer.WriteLine(sData);
                }
            }
        }

        // Thêm một loại hàng mới vào danh sách
        public string Them(LoaiHang loaihang)
        {
            List<LoaiHang> DSloaihang = DocDanhSach("");

            for (int i = 0; i < DSloaihang.Count; i++)
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

            DSloaihang.Insert(0, loaihang);

            LuuDanhSach(DSloaihang);

            return string.Empty;
        }

        // Sửa thông tin một loại hàng trong danh sách
        public string Sua(LoaiHang loaihangOld, LoaiHang loaihangNew)
        {
            List<LoaiHang> DSloaihang = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSloaihang.Count; i++)
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

            for (int i = 0; i < DSloaihang.Count; i++)
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

        // Xóa một loại hàng khỏi danh sách
        public string Xoa(LoaiHang loaihang)
        {
            List<LoaiHang> DSloaihang = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSloaihang.Count; i++)
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

            DSloaihang.RemoveAll(lh => lh.MaSo == loaihang.MaSo && lh.Ten == loaihang.Ten);

            LuuDanhSach(DSloaihang);

            return string.Empty;
        }

        // Đọc thông tin một loại hàng dựa trên mã số
        public LoaiHang? ReadInfo(string loaihangCode)
        {
            List<LoaiHang> DSloaihang = DocDanhSach("");

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

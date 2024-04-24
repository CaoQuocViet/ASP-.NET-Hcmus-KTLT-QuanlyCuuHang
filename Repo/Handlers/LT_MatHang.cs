using Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Repo
{
    public class LT_MatHang : ILT_MatHang
    {
        private const string _mathangFile = "Data/MatHang.json";

        // Đọc danh sách mặt hàng từ file và tìm kiếm theo từ khóa
        public List<MatHang> DocDanhSach(string sKeyword)
        {
            List<MatHang> DSmathang = new List<MatHang>();
            using (StreamReader reader = new StreamReader(_mathangFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    MatHang mathang = JsonConvert.DeserializeObject<MatHang>(line);
                    DSmathang.Add(mathang);
                }
            }

            if (string.IsNullOrEmpty(sKeyword))
            {
                return DSmathang;
            }

            List<MatHang> searchList = new List<MatHang>();
            foreach (MatHang mathang in DSmathang)
            {
                if (mathang.MaSo.Contains(sKeyword) ||
                    mathang.Ten.Contains(sKeyword) ||
                    mathang.LoaiHang.Contains(sKeyword))
                {
                    searchList.Add(mathang);
                }
            }

            return searchList;
        }

        // Lưu danh sách mặt hàng vào file
        public void LuuDanhSach(List<MatHang> DSmathang)
        {
            using (StreamWriter writer = new StreamWriter(_mathangFile))
            {
                foreach (MatHang mathang in DSmathang)
                {
                    string sData = JsonConvert.SerializeObject(mathang);
                    writer.WriteLine(sData);
                }
            }
        }

        // Thêm một mặt hàng mới vào danh sách
        public string Them(MatHang mathang)
        {
            List<MatHang> DSmathang = DocDanhSach("");

            for (int i = 0; i < DSmathang.Count; i++)
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

            DSmathang.Insert(0, mathang);

            LuuDanhSach(DSmathang);

            return string.Empty;
        }

        // Sửa thông tin một mặt hàng trong danh sách
        public string Sua(MatHang mathangOld, MatHang mathangNew)
        {
            List<MatHang> DSmathang = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSmathang.Count; i++)
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

            for (int i = 0; i < DSmathang.Count; i++)
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

            for (int i = 0; i < DSmathang.Count; i++)
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

        // Xóa một mặt hàng khỏi danh sách
        public string Xoa(MatHang mathang)
        {
            List<MatHang> DSmathang = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSmathang.Count; i++)
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

            DSmathang.RemoveAll(m => m.MaSo == mathang.MaSo && m.Ten == mathang.Ten);

            LuuDanhSach(DSmathang);

            return string.Empty;
        }

        // Đọc thông tin một mặt hàng dựa trên mã số
        public MatHang? ReadInfo(string sMatHangMaSo)
        {
            List<MatHang> DSmathang = DocDanhSach("");

            foreach (MatHang mathang in DSmathang)
            {
                if (sMatHangMaSo == mathang.MaSo)
                {
                    return mathang;
                }
            }

            return null;
        }

        // Cập nhật loại hàng của mặt hàng
        public void CapNhatLoaiHang(LoaiHang loaihangOld, LoaiHang loaihangNew)
        {
            List<MatHang> DSmathang = DocDanhSach("");

            for (int i = 0; i < DSmathang.Count; i++)
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

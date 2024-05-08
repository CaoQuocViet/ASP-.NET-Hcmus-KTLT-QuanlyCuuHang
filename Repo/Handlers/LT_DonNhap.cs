using Entities;
using Newtonsoft.Json;

namespace Repo
{
    public class LT_DonNhap : ILT_DonNhap
    {
        private const string _donnhapFile = "Data/DonNhap.json";

        // Đọc danh sách đơn nhập từ file và tìm kiếm theo từ khóa
        public List<DonNhap> DocDanhSach(string sKeyword)
        {
            List<DonNhap> DSdonnhap = new List<DonNhap>();
            using (StreamReader reader = new StreamReader(_donnhapFile))
            {
                string sData;
                while ((sData = reader.ReadLine()) != null)
                {
                    DSdonnhap.Add(JsonConvert.DeserializeObject<DonNhap>(sData)!);
                }
            }

            if (string.IsNullOrEmpty(sKeyword))
            {
                return DSdonnhap;
            }

            List<DonNhap> searchList = DSdonnhap.FindAll(d => d.MaSo.Contains(sKeyword));
            return searchList;
        }

        // Cập nhật danh sách đơn nhập với thông tin mặt hàng mới
        public void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
        {
            List<DonNhap> DSdonnhap = DocDanhSach("");

            foreach (DonNhap donnhap in DSdonnhap)
            {
                foreach (Kho kho in donnhap.Kho)
                {
                    if (mathangOld.Ten == kho.TenMatHang)
                    {
                        kho.TenMatHang = mathangNew.Ten;
                    }
                }
            }

            LuuDanhSach(DSdonnhap);
        }

        // Lưu danh sách đơn nhập vào file
        public void LuuDanhSach(List<DonNhap> DSdonnhap)
        {
            using (StreamWriter writer = new StreamWriter(_donnhapFile))
            {
                foreach (DonNhap donnhap in DSdonnhap)
                {
                    string sData = JsonConvert.SerializeObject(donnhap);
                    writer.WriteLine(sData);
                }
            }
        }

        // Thêm đơn nhập mới vào danh sách
        public string Them(DonNhap donnhap)
        {
            List<DonNhap> DSdonnhap = DocDanhSach("");

            for (int i = 0; i < DSdonnhap.Count; i++)
            {
                if (DSdonnhap[i].MaSo == donnhap.MaSo)
                {
                    return "Đơn nhập đã tồn tại";
                }
            }

            DSdonnhap.Insert(0, donnhap);

            LuuDanhSach(DSdonnhap);

            return string.Empty;
        }

        // Sửa thông tin đơn nhập trong danh sách
        public string Sua(DonNhap donnhapOld, DonNhap donnhapNew)
        {
            List<DonNhap> DSdonnhap = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSdonnhap.Count; i++)
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

            for (int i = 0; i < DSdonnhap.Count; i++)
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

        // Xóa đơn nhập khỏi danh sách
        public string Xoa(DonNhap donnhap)
        {
            List<DonNhap> DSdonnhap = DocDanhSach("");

            bool IsExist = false;
            for (int i = 0; i < DSdonnhap.Count; i++)
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

            DSdonnhap.RemoveAll(d => d.MaSo == donnhap.MaSo);

            LuuDanhSach(DSdonnhap);

            return string.Empty;
        }

        // Đọc thông tin đơn nhập dựa trên mã số
        public DonNhap? DocThongTin(string donnhapMaSo)
        {
            List<DonNhap> DSdonnhap = DocDanhSach("");

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

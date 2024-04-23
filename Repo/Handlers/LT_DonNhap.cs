using Entities;
using Newtonsoft.Json;

namespace Repo
{
    public class LT_DonNhap : ILT_DonNhap
    {
        private const string _donnhapFile = "Data/DonNhap.json";

        // Đọc danh sách đơn nhập từ file và tìm kiếm theo từ khóa
        public DonNhap[] DocDanhSach(string sKeyword)
        {
            StreamReader reader = new StreamReader(_donnhapFile);
            int count = 0;
            while (null != reader.ReadLine())
            {
                count++;
            }
            reader.Close();

            DonNhap[] DSdonnhap = new DonNhap[count];
            reader = new StreamReader(_donnhapFile);
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

        // Cập nhật danh sách đơn nhập với thông tin mặt hàng mới
        public void CapNhatDS(MatHang mathangOld, MatHang mathangNew)
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

        // Lưu danh sách đơn nhập vào file
        public void LuuDanhSach(DonNhap[] DSdonnhap)
        {
            StreamWriter writer = new StreamWriter(_donnhapFile);
            foreach (DonNhap donnhap in DSdonnhap)
            {
                string sData = JsonConvert.SerializeObject(donnhap);
                writer.WriteLine(sData);
            }
            writer.Close();
        }

        // Thêm đơn nhập mới vào danh sách
        public string Them(DonNhap donnhap)
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

        // Sửa thông tin đơn nhập trong danh sách
        public string Sua(DonNhap donnhapOld, DonNhap donnhapNew)
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

        // Xóa đơn nhập khỏi danh sách
        public string Xoa(DonNhap donnhap)
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

        // Đọc thông tin đơn nhập dựa trên mã số
        public DonNhap? ReadInfo(string donnhapMaSo)
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

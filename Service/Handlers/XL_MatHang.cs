using Repo;
using Entities;
using System.Collections.Generic;

namespace Service
{
    public class XL_MatHang : IXL_MatHang
    {
        private ILT_MatHang _luuTruMatHang = new LT_MatHang();
        private const int _maSoMaxLength = 1000;

        // Hàm để đọc danh sách mặt hàng dựa trên từ khóa
        public List<MatHang> DocDanhSach(string sKeyword)
        {
            return _luuTruMatHang.DocDanhSach(sKeyword);  
        }

        // Hàm để cập nhật loại hàng của mặt hàng
        public void CapNhatLoaiHang(LoaiHang loaihangOld, LoaiHang loaihangNew)
        {
            _luuTruMatHang.CapNhatLoaiHang(loaihangOld, loaihangNew);  
        }

        // Hàm để thêm một mặt hàng mới
        public string Them(string sMaSo, string sTen, string sLoaiHang, string sGia, ref MatHang mathang)
        {
            // Kiểm tra và gán các thuộc tính của mặt hàng
            mathang.MaSo = sMaSo;
            mathang.Ten = sTen;
            mathang.LoaiHang = sLoaiHang;

            // Kiểm tra mã số mặt hàng có hợp lệ hay không
            if (mathang.MaSo.Length == 0 || mathang.MaSo.Length > _maSoMaxLength)
            {
                return "Mã mặt hàng không hợp lệ";
            }

            // Kiểm tra và gán giá mặt hàng
            int parsedGia;
            if (!int.TryParse(sGia, out parsedGia))
            {
                return "Giá mặt hàng không hợp lệ";
            }
            mathang.Gia = parsedGia;

            // Thêm mặt hàng vào cơ sở dữ liệu
            return _luuTruMatHang.Them(mathang); 
        }

        // Hàm để sửa thông tin một mặt hàng
        public string Sua(string sMaSo, string sTen, string sLoaiHang, string sGia, string sThuongHieu, ref MatHang mathangOld)
        {
            // Tạo một đối tượng mặt hàng mới với thông tin cần sửa
            MatHang mathang = new MatHang(sMaSo, sTen, sLoaiHang, sThuongHieu, 0)
            {
                MaSo = sMaSo,
                Ten = sTen,
                LoaiHang = sLoaiHang,
                ThuongHieu = sThuongHieu
            };

            // Kiểm tra mã số mặt hàng có hợp lệ hay không
            if (mathang == null || mathang.MaSo.Length == 0 || mathang.MaSo.Length > _maSoMaxLength)
            {
                return "Mã mặt hàng không hợp lệ";
            }

            // Kiểm tra loại hàng có tồn tại hay không
            XL_LoaiHang xlLoaiHang = new XL_LoaiHang(); 
            List<LoaiHang> DSloaihang = xlLoaiHang.DocDanhSach(""); 
            bool isValid = false;
            for (int i = 0; i < DSloaihang.Count; i++)
            {
                if (mathang.LoaiHang == DSloaihang[i].Ten)
                {
                    isValid = true;
                    break;
                }
            }
            if (!isValid)
            {
                return "Loại hàng không tồn tại";
            }

            // Kiểm tra và gán giá mặt hàng
            int parsedGia;
            if (!int.TryParse(sGia, out parsedGia))
            {
                return "Giá mặt hàng không hợp lệ";
            }
            mathang.Gia = parsedGia;

            // Cập nhật thông tin mặt hàng trong cơ sở dữ liệu
            string sInfo = ""; 
            if (string.IsNullOrEmpty(sInfo))
            {
                XL_Kho kho = new XL_Kho(); 
                kho.CapNhatDS(mathangOld, mathang); 
                XL_DonNhap xlDonNhap = new XL_DonNhap(); 
                xlDonNhap.CapNhatDS(mathangOld, mathang); 
                XL_DonXuat xlDonXuat = new XL_DonXuat(); 
                xlDonXuat.CapNhatDS(mathangOld, mathang); 
                mathangOld = mathang;
            }

            return sInfo;
        }

        // Hàm để xóa một mặt hàng
        public string Xoa(string sMaSo, string sTen, string sLoaiHang, string sThuongHieu, string sGia)
        {
            // Tạo một đối tượng mặt hàng để xóa
            MatHang mathang = new MatHang(sMaSo, sTen, sLoaiHang, sThuongHieu, 0)
            {
                MaSo = sMaSo,
                Ten = sTen,
                LoaiHang = sLoaiHang,
                ThuongHieu = sThuongHieu
            };

            // Kiểm tra mã số mặt hàng có hợp lệ hay không
            if (mathang.MaSo.Length == 0 || mathang.MaSo.Length > _maSoMaxLength)
            {
                return "Mã mặt hàng không hợp lệ";
            }

            // Kiểm tra mặt hàng có được sử dụng trong đơn nhập hay không
            if (string.IsNullOrEmpty(new XL_DonNhap().MatHangTonTai(mathang)))
            {
                return "Không thể xóa mặt hàng được sử dụng trong đơn nhập";
            }

            // Kiểm tra mặt hàng có được sử dụng trong đơn xuất hay không
            XL_DonXuat xlDonXuat = new XL_DonXuat();  
            if (string.IsNullOrEmpty(xlDonXuat.MatHangTonTai(mathang)))
            {
                return "Không thể xóa mặt hàng được sử dụng trong đơn xuất";
            }

            // Xóa mặt hàng khỏi cơ sở dữ liệu
            return _luuTruMatHang.Xoa(mathang);
        }

        // Hàm để đọc thông tin một mặt hàng dựa trên mã số
        public MatHang ReadInfo(string sMatHangMaSo)
        {
            return _luuTruMatHang.ReadInfo(sMatHangMaSo);  
        }
    }
}

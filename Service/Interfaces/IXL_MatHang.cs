using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repo;
using Entities;

namespace Service
{
    public interface IXL_MatHang : IService<MatHang>
    {
        // MatHang[] DocDanhSach(string sKeyword);
        void CapNhatLoaiHang(LoaiHang loaihangOld, LoaiHang loaihangNew);
        string Them(string sMaSo, string sTen, string sLoaiHang, string sGia, ref MatHang mathang);
        string Sua(string sMaSo, string sTen, string sLoaiHang, string sGia, string sThuongHieu, ref MatHang mathangOld);
        string Xoa(string sMaSo, string sTen, string sLoaiHang, string sThuongHieu, string sGia);
        // MatHang? ReadInfo(string sMatHangMaSo);
    }
}

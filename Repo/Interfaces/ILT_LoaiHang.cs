using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Repo
{
    public interface ILT_LoaiHang
    {
        LoaiHang[] DocDanhSach(string sKeyword);
        void LuuDanhSach(LoaiHang[] DSloaihang);
        string Them(LoaiHang loaihang);
        string Sua(LoaiHang loaihangOld, LoaiHang loaihangNew);
        string Xoa(LoaiHang loaihang);
        LoaiHang? ReadInfo(string loaihangCode);
    }
}

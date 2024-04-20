using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repo;
using Entities;

namespace Service
{
    public interface IXL_LoaiHang
    {
        LoaiHang Empty();
        LoaiHang[] DocDanhSach(string sKeyword);
        string Them(LoaiHang loaihang);
        string Sua(string sMaSo, string sTen, ref LoaiHang loaihangOld);
        string Xoa(string sMaSo, string sTen);
        LoaiHang? ReadInfo(string loaihangCode);
    }
}

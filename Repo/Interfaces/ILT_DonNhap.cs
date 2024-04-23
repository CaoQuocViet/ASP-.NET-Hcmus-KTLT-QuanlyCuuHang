using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Repo
{
    public interface ILT_DonNhap : IRepository<DonNhap>
    {
        public void CapNhatDS(MatHang mathangOld, MatHang mathangNew);
        
        // DonNhap[] DocDanhSach(string sKeyword);
        // void CapNhatDS(MatHang mathangOld, MatHang mathangNew);
        // string Them(DonNhap donnhap);
        // string Sua(DonNhap donnhapOld, DonNhap donnhapNew);
        // string Xoa(DonNhap donnhap);
        // DonNhap? ReadInfo(string donnhapMaSo);
    }
}

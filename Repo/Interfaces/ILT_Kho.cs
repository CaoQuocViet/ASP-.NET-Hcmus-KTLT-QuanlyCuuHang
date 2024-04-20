using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Repo
{
    public interface ILT_Kho
    {
        Kho[] DocDanhSach(string sKeyword);
        void CapNhatDS(MatHang mathangOld, MatHang mathangNew);
        void DonNhap(DonNhap donnhap);
        void DonXuat(DonXuat donxuat);
    }
}

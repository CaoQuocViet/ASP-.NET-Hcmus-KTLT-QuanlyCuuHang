using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repo;
using Entities;

namespace Service
{
    public interface IXL_DonNhap
    {
        DonNhap[] DocDanhSach(string sKeyword);
        void CapNhatDS(MatHang mathangOld, MatHang mathangNew);
        string MatHangTonTai(MatHang mathang);
        string Them(string sMaSo, string sNgay, Kho[] DSkho, ref DonNhap donnhap);
        string Sua(string sMaSo, string sNgay, ref DonNhap donnhapOld);
        DonNhap? ReadInfo(string donnhapMaSo);
    }
}

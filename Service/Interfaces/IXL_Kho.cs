using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repo;
using Entities;

namespace Service
{
    public interface IXL_Kho
    {
        Kho[] DocDanhSach(string sKeyword, int Filter);
        void CapNhatDS(MatHang mathangOld, MatHang mathangNew);
        string XacMinhNhapKho(string sTenMatHang, string sSoLuong, string sNgaySanXuat, string sHanDung, ref Kho kho);
        string XacMinhXuatKho(string sTenMatHang, string sSoLuong, string sNgaySanXuat, string sHanDung, ref Kho kho);
        string ThemVaoDS(Kho kho, ref Kho[] DSkho);
        void KiemTraHangHoaTonTai(string sKhoList, ref Kho[] DSkho);
        string TaoDanhSachHangHoa(Kho[] DSkho);
        void DonNhap(DonNhap donnhap);
        void DonXuat(DonXuat donxuat);
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_DonHangNhap_ThemModel : PageModel
    {
        public string? sInfoKho { get; set; }
        public string? sInfoDonNhap { get; set; }
        public DonNhap? import { get; set; }
        public Kho? kho { get; set; }
        public Kho[]? DSkho { get; set; }
        public string? sKhoList { get; set; }
        public int? formId { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Quản lý cửa hàng";
            sInfoKho = string.Empty;
            sInfoDonNhap = string.Empty;
            import = new DonNhap();
            kho = new Kho();
            DSkho = new Kho[0];
            sKhoList = string.Empty;
            formId = 0;
        }

        public void OnPost()
        {
            sKhoList = Request.Form["khos"];
            XL_Kho xl_Kho = new XL_Kho();
            XL_Kho khoHelper = new XL_Kho();
            Kho[] tempDSkho = DSkho ?? new Kho[0];
            khoHelper.KiemTraHangHoaTonTai(sKhoList, ref tempDSkho);
            DSkho = tempDSkho;

            var formValue = Request.Form["form"];
            int formId;
            if (int.TryParse(formValue, out formId))
            {
                if (formId == 1)
                {
                    string sMaSo = Request.Form["id"];
                    string sNgay = Request.Form["date"];
                    XL_DonNhap xl_DonNhap = new XL_DonNhap();
                    DonNhap tempImport = import ?? new DonNhap();
                    sInfoDonNhap = xl_DonNhap.Them(sMaSo, sNgay, DSkho, ref tempImport);
                    import = tempImport;

                    if (string.IsNullOrEmpty(sInfoDonNhap))
                    {
                        import = new DonNhap();
                        kho = new Kho();
                        DSkho = new Kho[0];
                        sKhoList = string.Empty;
                    }
                }
                else if (formId == 2)
                {
                    string sTenMatHang = Request.Form["pname"];
                    string sSoLuong = Request.Form["quantity"];
                    string sNgaySanXuat = Request.Form["mdate"];
                    string sHanDung = Request.Form["edate"];
                    Kho tempKho = kho ?? new Kho(); 
                    if (tempKho != null)
                    {
                        sInfoKho = xl_Kho.XacMinhNhapKho(sTenMatHang, sSoLuong, sNgaySanXuat, sHanDung, ref tempKho);
                    }
                }
            }
            Kho[] tempDSkho3 = DSkho;
            xl_Kho.KiemTraHangHoaTonTai(sKhoList, ref tempDSkho3);
            DSkho = tempDSkho3;
        }
    }
}

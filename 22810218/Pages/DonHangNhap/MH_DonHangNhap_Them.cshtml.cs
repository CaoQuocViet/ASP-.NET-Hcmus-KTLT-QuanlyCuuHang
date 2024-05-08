using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;
using System.Collections.Generic;

namespace Pages
{
    public class MH_DonHangNhap_ThemModel : PageModel
    {
        public string? sInfoKho { get; set; }
        public string? sInfoDonNhap { get; set; }
        public DonNhap? import { get; set; }
        public Kho? kho { get; set; }
        public List<Kho>? DSkho { get; set; }
        public string? sKhoList { get; set; }
        public int? formId { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Quản lý cửa hàng";
            sInfoKho = string.Empty;
            sInfoDonNhap = string.Empty;
            import = new DonNhap("", default, new List<Kho>()); // Provide the required arguments for the 'DonNhap' constructor
            kho = new Kho("", 0, default, default); // Provide the required arguments for the 'Kho' constructor
            DSkho = new List<Kho>();
            sKhoList = string.Empty;
            formId = 0;
        }

        public void OnPost()
        {
            sKhoList = Request.Form["khos"];
            XL_Kho xl_Kho = new XL_Kho();
            XL_Kho khoHelper = new XL_Kho();
            List<Kho> tempDSkho = DSkho ?? new List<Kho>();
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
                    DonNhap tempImport = import ?? new DonNhap("", default, new List<Kho>());
                    sInfoDonNhap = xl_DonNhap.Them(sMaSo, sNgay, DSkho, ref tempImport);
                    import = tempImport;

                    if (string.IsNullOrEmpty(sInfoDonNhap))
                    {
                        import = new DonNhap("", default, new List<Kho>());
                        kho = new Kho("", 0, default, default);
                        DSkho = new List<Kho>();
                        sKhoList = string.Empty;
                    }
                }
                else if (formId == 2)
                {
                    string sTenMatHang = Request.Form["pname"];
                    string sSoLuong = Request.Form["quantity"];
                    string sNgaySanXuat = Request.Form["mdate"];
                    string sHanDung = Request.Form["edate"];
                    Kho tempKho = kho ?? new Kho("", 0, default, default); 
                    if (tempKho != null)
                    {
                        sInfoKho = xl_Kho.XacMinhNhapKho(sTenMatHang, sSoLuong, sNgaySanXuat, sHanDung, ref tempKho);
                    }
                }
            }
            List<Kho> tempDSkho3 = DSkho ?? new List<Kho>();
            xl_Kho.KiemTraHangHoaTonTai(sKhoList, ref tempDSkho3);
            DSkho = tempDSkho3;
            xl_Kho.KiemTraHangHoaTonTai(sKhoList, ref tempDSkho3);
            DSkho = tempDSkho3;
        }
    }
}

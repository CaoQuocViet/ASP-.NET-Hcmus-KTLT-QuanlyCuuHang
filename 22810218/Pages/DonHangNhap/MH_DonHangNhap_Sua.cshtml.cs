using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_DonHangNhap_SuaModel : PageModel
    {
        public string? sInfo { get; set; }
        public bool bFlag { get; set; }
        public DonNhap? import { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Quản lý cửa hàng";
            sInfo = string.Empty;
            bFlag = false;
            import = new DonNhap("", new DateOnly(), new List<Kho>());
            string importMaSo = Request.Query["id"];
            XL_DonNhap xlDonNhap = new XL_DonNhap();
            DonNhap? isDonNhap = xlDonNhap.DocThongTin(importMaSo);
            if (isDonNhap == null)
            {
                sInfo = "Đơn nhập này không tồn tại";
                bFlag = true;
            }
            else
            {
                import = isDonNhap;
            }
        }

        public void OnPost()
        {
            bFlag = true;
            string sMaSo = Request.Form["id"];
            string sNgay = Request.Form["date"];
            XL_DonNhap xlDonNhap = new XL_DonNhap();
            DonNhap tempImport = new DonNhap(sMaSo, new DateOnly(), new List<Kho>());
            sInfo = xlDonNhap.Sua(sMaSo, sNgay, ref tempImport);
            import = tempImport;
        }
    }
}

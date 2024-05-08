using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_DonHangNhap_ChiTietModel : PageModel
    {
        public DonNhap? Import { get; private set; }
        public string? ImportMaSo { get; private set; }

        public void OnGet()
        {
            ViewData["Title"] = "Quản lý cửa hàng";
            ImportMaSo = Request.Query["id"];
            Import = new XL_DonNhap().DocThongTin(ImportMaSo);
        }
    }
}

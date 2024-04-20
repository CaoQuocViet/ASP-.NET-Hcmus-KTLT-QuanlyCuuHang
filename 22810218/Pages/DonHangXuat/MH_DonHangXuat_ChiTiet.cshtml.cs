using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_DonHangXuat_ChiTietModel : PageModel
    {
        public DonXuat? DonXuat { get; private set; }

        public void OnGet()
        {
            ViewData["Title"] = "Quản lý cửa hàng";
            string donxuatMaSo = Request.Query["id"];
            DonXuat = new XL_DonXuat().ReadInfo(donxuatMaSo);
        }
    }
}

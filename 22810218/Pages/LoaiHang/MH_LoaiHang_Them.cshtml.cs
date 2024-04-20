using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_LoaiHang_ThemModel : PageModel
    {
        public LoaiHang? LoaiHang { get; private set; }
        public string? SInfo { get; private set; }
        public bool BFlag { get; private set; }

        public void OnGet()
        {
            ViewData["Title"] = "Quản lý cửa hàng";
            LoaiHang = new LoaiHang("", "");
            BFlag = false;
        }

        public void OnPost()
        {
            BFlag = true;
            LoaiHang = new LoaiHang(Request.Form["id"], Request.Form["name"]);
            XL_LoaiHang xlLoaiHang = new XL_LoaiHang();
            SInfo = xlLoaiHang.Them(LoaiHang);
            if (string.IsNullOrEmpty(SInfo))
            {
                LoaiHang = new LoaiHang("", "");
            }
        }
    }
}

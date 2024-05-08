using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_LoaiHang_XoaModel : PageModel
    {
        public string? SInfo { get; private set; }
        public bool BFlag { get; private set; }
        public LoaiHang? LoaiHang { get; private set; }

        public IActionResult OnGet()
        {
            ViewData["Title"] = "Quản lý cửa hàng";
            string sLoaiHangMaSo = Request.Query["id"];
            XL_LoaiHang xlLoaiHang = new XL_LoaiHang();
            LoaiHang? isLoaiHang = xlLoaiHang.DocThongTin(sLoaiHangMaSo);

            if (isLoaiHang == null)
            {
                SInfo = "Loại hàng này không tồn tại";
                BFlag = true;
            }
            else
            {
                BFlag = false;
                LoaiHang = isLoaiHang;
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            BFlag = true;
            string sMaSo = Request.Form["id"];
            string sTen = Request.Form["name"];
            XL_LoaiHang xlLoaiHang = new XL_LoaiHang();
            SInfo = xlLoaiHang.Xoa(sMaSo, sTen);
            if (string.IsNullOrEmpty(SInfo))
            {
                return RedirectToPage("/LoaiHang/MH_LoaiHang_DanhSach");
            }
            else
            {
                return Page();
            }
        }
    }
}

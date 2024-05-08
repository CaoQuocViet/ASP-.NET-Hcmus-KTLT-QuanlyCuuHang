using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_MatHang_ThemModel : PageModel
    {
        [BindProperty]
        public MatHang? Mathang { get; set; }
        public string? SInfo { get; set; }
        public bool BFlag { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Quản lý cửa hàng";
            Mathang = new MatHang("", "", "", "", 0);
        }

        public IActionResult OnPost()
        {
            BFlag = true;

            string sMaSo = Request.Form["id"];
            string sTen = Request.Form["name"];
            string sLoaiHang = Request.Form["loaihang"];
            string sThuongHieu = Request.Form["brand"];
            string sGia = Request.Form["price"];

            int parsedGia;
            if (!int.TryParse(sGia, out parsedGia))
            {
                SInfo = "Giá mặt hàng không hợp lệ";
                return Page();
            }

            MatHang tempMatHang = new MatHang(sMaSo, sTen, sLoaiHang, sThuongHieu, parsedGia);

            XL_MatHang xlMatHang = new XL_MatHang();
            SInfo = xlMatHang.Them(sMaSo, sTen, sLoaiHang, sGia, sThuongHieu, ref tempMatHang);

            if (string.IsNullOrEmpty(SInfo))
            {
                Mathang = tempMatHang;
            }

            return Page();
        }
    }
}

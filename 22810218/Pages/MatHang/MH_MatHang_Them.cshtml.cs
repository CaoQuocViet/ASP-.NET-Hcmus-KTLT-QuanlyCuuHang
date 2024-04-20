using System;
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

        public void OnGet()
        {
            Mathang = new MatHang("", "", "", "", 0);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string sMaSo = Request.Form["id"];
            string sTen = Request.Form["name"];
            string sLoaiHang = Request.Form["loaihang"];
            string sThuongHieu = Request.Form["brand"];
            string sGia = Request.Form["price"];

            XL_MatHang xlMatHang = new XL_MatHang();
            MatHang tempMatHang = Mathang ?? new MatHang("", "", "", "", 0);
            SInfo = xlMatHang.Them(sMaSo, sTen, sLoaiHang, sGia, ref tempMatHang);
            Mathang = tempMatHang;

            if (string.IsNullOrEmpty(SInfo))
            {
                Mathang = new MatHang("", "", "", "", 0) { MaSo = "", Ten = "", LoaiHang = "", Gia = 0 };
            }

            return Page();
        }
    }
}

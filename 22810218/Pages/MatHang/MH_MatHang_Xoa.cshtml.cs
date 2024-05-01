using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_MatHang_XoaModel : PageModel
    {
        public string? SInfo { get; private set; }
        public bool BFlag { get; private set; }
        public MatHang? MatHang { get; private set; }

        public void OnGet()
        {
            ViewData["Title"] = "Quản lý cửa hàng";
            string sMatHangMaSo = Request.Query["id"];
            MatHang = new MatHang("", "", "", "", 0); // Pass the required arguments to the constructor
            XL_MatHang xlMatHang = new XL_MatHang();
            MatHang? isMatHang = xlMatHang.ReadInfo(sMatHangMaSo);
            if (isMatHang == null)
            {
                SInfo = "This mathang is not exists";
                BFlag = true;
            }
        }

        public IActionResult OnPost()
        {
            BFlag = true;
            string sMaSo = Request.Form["id"];
            string sTen = Request.Form["name"];
            string sLoaiHang = Request.Form["loaihang"];
            string sThuongHieu = Request.Form["brand"];
            string sGia = Request.Form["price"];
            XL_MatHang xlMatHang = new XL_MatHang();
            SInfo = xlMatHang.Xoa(sMaSo, sTen, sLoaiHang, sThuongHieu, sGia);
            if (string.IsNullOrEmpty(SInfo))
            {
                return RedirectToPage("/mathang/index");
            }
            else
            {
                return Page();
            }
        }
    }
}

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

        public IActionResult OnGet()
        {
            ViewData["Title"] = "Quản lý cửa hàng";
            string sMatHangMaSo = Request.Query["id"];
            MatHang = new MatHang("", "", "", "", 0);
            XL_MatHang xlMatHang = new XL_MatHang();
            MatHang? isMatHang = xlMatHang.DocThongTin(sMatHangMaSo);
            if (isMatHang == null)
            {
                SInfo = "Mặt hàng này không tồn tại";
                BFlag = true;
            }
            else
            {
                MatHang = isMatHang;
            }
            return Page();
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
                return RedirectToPage("/MatHang/MH_MatHang_DanhSach");
            }
            else
            {
                return Page();
            }
        }
    }
}

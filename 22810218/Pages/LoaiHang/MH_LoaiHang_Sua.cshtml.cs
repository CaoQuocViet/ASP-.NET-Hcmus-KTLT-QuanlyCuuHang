using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_LoaiHang_SuaModel : PageModel
    {
        [BindProperty]
        public LoaiHang? LoaiHang { get; set; }

        public string? SInfo { get; set; }
        public bool BFlag { get; set; }

        public void OnGet(string id)
        {
            ViewData["Title"] = "Quản lý cửa hàng";
            BFlag = false;
            LoaiHang = new LoaiHang("", ""); // Pass the required arguments to the constructor

            string sLoaiHangMaSo = id;

            XL_LoaiHang xlLoaiHang = new XL_LoaiHang();
            LoaiHang? isLoaiHang = xlLoaiHang.ReadInfo(sLoaiHangMaSo);

            if (null == isLoaiHang)
            {
                SInfo = "This loaihang is not exists";
                BFlag = true;
            }
        }

        public IActionResult OnPost()
        {
            BFlag = true;
            XL_LoaiHang xlLoaiHang = new XL_LoaiHang();
            string sMaSo = Request.Form["id"];
            string sTen = Request.Form["name"];
            LoaiHang tempLoaiHang = LoaiHang ?? new LoaiHang("", ""); // Pass the required arguments to the constructor
            SInfo = xlLoaiHang.Sua(sMaSo, sTen, ref tempLoaiHang);
            LoaiHang = tempLoaiHang;

            return Page();
        }
    }
}

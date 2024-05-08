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

            if (string.IsNullOrEmpty(id))
            {
                SInfo = "Mã loại hàng không được cung cấp";
                BFlag = true;
            }
            else
            {
                XL_LoaiHang xlLoaiHang = new XL_LoaiHang();
                LoaiHang = xlLoaiHang.DocThongTin(id);

                if (LoaiHang == null)
                {
                    SInfo = "Loại hàng này không tồn tại";
                    BFlag = true;
                }
            }
        }

        public IActionResult OnPost()
        {
            BFlag = true;
            XL_LoaiHang xlLoaiHang = new XL_LoaiHang();
            string sMaSo = Request.Form["LoaiHang.MaSo"];
            string sTen = Request.Form["LoaiHang.Ten"];
            LoaiHang tempLoaiHang = LoaiHang ?? new LoaiHang("", ""); 
            SInfo = xlLoaiHang.Sua(sMaSo, sTen, ref tempLoaiHang);
            LoaiHang = tempLoaiHang;

            return Page();
        }
    }
}

    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Entities;
    using Service;

    namespace Pages
    {
        public class MH_MatHang_SuaModel : PageModel
        {
            public string? sInfo { get; set; }
            public bool? bFlag { get; set; }
            public MatHang? mathang { get; set; }

            public void OnGet()
            {
                ViewData["Title"] = "Quản lý cửa hàng";
                sInfo = string.Empty;
                bFlag = false;
                mathang = new MatHang("", "", "", "", 0);

                string sMatHangMaSo = Request.Query["id"];
                XL_MatHang xlMatHang = new XL_MatHang();
                MatHang? isMatHang = xlMatHang.ReadInfo(sMatHangMaSo);

                if (null == isMatHang)
                {
                    sInfo = "This mathang is not exists";
                    bFlag = true;
                }
                else if (isMatHang is MatHang matHang)
                {
                    mathang = matHang;
                }
            }

            public void OnPost()
            {
                bFlag = true;
                string sMaSo = Request.Form["id"];
                string sTen = Request.Form["name"];
                string sLoaiHang = Request.Form["loaihang"];
                string sThuongHieu = Request.Form["brand"];
                string sGia = Request.Form["price"];

                XL_MatHang xlMatHang = new XL_MatHang();

                MatHang tempMatHang = mathang ?? new MatHang("", "", "", "", 0);
                sInfo = xlMatHang.Sua(sMaSo, sTen, sLoaiHang, sGia, sThuongHieu, ref tempMatHang);
                mathang = tempMatHang;
            }
        }
    }

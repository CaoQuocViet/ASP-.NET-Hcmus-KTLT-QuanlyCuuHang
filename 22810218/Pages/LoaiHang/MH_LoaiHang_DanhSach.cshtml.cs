using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_LoaiHang_DanhSachModel : PageModel
    {
        public string? ViewDataTitle { get; set; }
        public int ItemPerPage { get; private set; } = 10;
        public int PageIndex { get; private set; }
        public int PageTotal { get; private set; } = 1;
        public int NoStart { get; private set; } = 1;
        public int NoEnd { get; private set; } = 1;
        public int NoTotal { get; private set; } = 1;
        public string SKeyword { get; private set; } = string.Empty;
        public LoaiHang[]? DSLoaihang { get; private set; }

        public void OnGet()
        {
            ViewDataTitle = "Quản lý cửa hàng";
            int parsedPageIndex;
            int.TryParse(Request.Query["page"], out parsedPageIndex);
            PageIndex = parsedPageIndex;
            SKeyword = Request.Query["search"];
            XL_LoaiHang xlLoaiHang = new XL_LoaiHang();
            DSLoaihang = xlLoaiHang.DocDanhSach(SKeyword);

            if (DSLoaihang.Length > 0)
            {
                PageTotal = ((int)DSLoaihang.Length - 1) / ItemPerPage + 1;

                if (PageIndex == 0 || PageIndex > PageTotal)
                {
                    PageIndex = 1;
                }

                NoStart = (PageIndex - 1) * ItemPerPage + 1;
                NoEnd = NoStart + ItemPerPage - 1;
                NoTotal = (int)DSLoaihang.Length;
                if (PageIndex == PageTotal)
                {
                    NoEnd = NoStart + ((int)DSLoaihang.Length - 1) % ItemPerPage;
                }
            }
        }
    }
}

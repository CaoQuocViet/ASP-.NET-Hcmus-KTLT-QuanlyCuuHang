using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_MatHang_DanhSachModel : PageModel
    {
        public MatHang[]? DSMatHang { get; private set; }
        public int PageIndex { get; private set; }
        public int PageTotal { get; private set; }
        public int NoStart { get; private set; }
        public int NoEnd { get; private set; }
        public int NoTotal { get; private set; }
        public string? SKeyword { get; private set; }

        public void OnGet()
        {
            ViewData["Title"] = "Quản lý cửa hàng";
            const int itemPerPage = 10;
            PageIndex = 1;
            SKeyword = Request.Query["search"];

            string pageValue = Request.Query["page"];
            int.TryParse(pageValue, out int pageIndex);
            PageIndex = pageIndex;

            XL_MatHang xlMatHang = new XL_MatHang();
            DSMatHang = xlMatHang.DocDanhSach(SKeyword);

            if (DSMatHang.Length > 0)
            {
                PageTotal = ((int)DSMatHang.Length - 1) / itemPerPage + 1;
                if (PageIndex == 0 || PageIndex > PageTotal)
                {
                    PageIndex = 1;
                }
                NoStart = (PageIndex - 1) * itemPerPage + 1;
                NoEnd = NoStart + itemPerPage - 1;
                NoTotal = (int)DSMatHang.Length;
                if (PageIndex == PageTotal)
                {
                    NoEnd = NoStart + ((int)DSMatHang.Length - 1) % itemPerPage;
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_DonHangXuat_DanhSachModel : PageModel
    {
        public DonXuat[]? DSdonxuat { get; private set; }
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

            int.TryParse(Request.Query["page"], out int pageIndex);
            SKeyword = Request.Query["search"];

            XL_DonXuat xlDonXuat = new XL_DonXuat();
            DSdonxuat = xlDonXuat.DocDanhSach(SKeyword);

            if (DSdonxuat.Length > 0)
            {
                PageTotal = ((int)DSdonxuat.Length - 1) / itemPerPage + 1;

                if (pageIndex == 0 || pageIndex > PageTotal)
                {
                    pageIndex = 1;
                }

                NoStart = (pageIndex - 1) * itemPerPage + 1;
                NoEnd = NoStart + itemPerPage - 1;
                NoTotal = (int)DSdonxuat.Length;
                if (pageIndex == PageTotal)
                {
                    NoEnd = NoStart + ((int)DSdonxuat.Length - 1) % itemPerPage;
                }
            }

            PageIndex = pageIndex;
        }
    }
}

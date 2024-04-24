using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;
using System.Collections.Generic;

namespace Pages
{
    public class MH_DonHangXuat_DanhSachModel : PageModel
    {
        public List<DonXuat>? DSdonxuat { get; private set; }
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

            if (DSdonxuat.Count > 0)
            {
                PageTotal = (DSdonxuat.Count - 1) / itemPerPage + 1;

                if (pageIndex == 0 || pageIndex > PageTotal)
                {
                    pageIndex = 1;
                }

                NoStart = (pageIndex - 1) * itemPerPage + 1;
                NoEnd = NoStart + itemPerPage - 1;
                NoTotal = DSdonxuat.Count;
                if (pageIndex == PageTotal)
                {
                    NoEnd = NoStart + (DSdonxuat.Count - 1) % itemPerPage;
                }
            }

            PageIndex = pageIndex;
        }
    }
}

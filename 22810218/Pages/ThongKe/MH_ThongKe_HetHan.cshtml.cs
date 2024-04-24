using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;
using System.Collections.Generic;

namespace Pages
{
    public class MH_ThongKe_HetHanModel : PageModel
    {
        public List<Kho>? DSkho { get; private set; }
        public int PageIndex { get; private set; }
        public int PageTotal { get; private set; }
        public int NoStart { get; private set; }
        public int NoEnd { get; private set; }
        public int NoTotal { get; private set; }
        public string? SKeyword { get; private set; }

        public void OnGet()
        {
            ViewData["Title"] = "Thống kê Hàng hết hạn";
            const int itemPerPage = 10;
            PageIndex = 1;
            PageTotal = 1;
            NoStart = 1;
            NoEnd = 1;
            NoTotal = 1;
            SKeyword = string.Empty;
            string sFilter = string.Empty;
            int Filter = 2;

            string pageValue = Request.Query["page"];
            int.TryParse(pageValue, out int pageIndex);
            PageIndex = pageIndex;

            sFilter = Request.Query["filter"];

            if ("unexpired" == sFilter)
            {
                Filter = 1;
            }

            SKeyword = Request.Query["search"];

            XL_Kho kho = new XL_Kho();
            DSkho = kho.DocDanhSach(SKeyword, Filter);

            if (DSkho.Count > 0)
            {
                PageTotal = (DSkho.Count - 1) / itemPerPage + 1;

                if (PageIndex == 0 || PageIndex > PageTotal)
                {
                    PageIndex = 1;
                }

                NoStart = (PageIndex - 1) * itemPerPage + 1;
                NoEnd = NoStart + itemPerPage - 1;
                NoTotal = DSkho.Count;
                if (PageIndex == PageTotal)
                {
                    NoEnd = NoStart + (DSkho.Count - 1) % itemPerPage;
                }
            }
        }
    }
}

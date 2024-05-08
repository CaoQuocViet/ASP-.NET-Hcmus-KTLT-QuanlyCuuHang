using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;
using System.Collections.Generic;

namespace Pages
{
    public class MH_ThongKe_TonKhoModel : PageModel
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
            ViewData["Title"] = "Thống kê Hàng tồn kho";
            const int itemPerPage = 10;
            int pageIndex = 1; 
            PageTotal = 1;
            NoStart = 1;
            NoEnd = 1;
            NoTotal = 1;
            SKeyword = string.Empty;
            int Filter = 1;

            string pageValue = Request.Query["page"];
            int.TryParse(pageValue, out pageIndex);
            PageIndex = pageIndex;

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

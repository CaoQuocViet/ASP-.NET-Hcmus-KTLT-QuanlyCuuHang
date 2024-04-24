using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;
using System.Collections.Generic;

namespace Pages
{
    public class ChiTietKhoHangModel : PageModel
    {
        public List<Kho>? DSkho { get; set; }
        public string? sKeyword { get; set; }
        public int pageIndex { get; set; }
        public int pageTotal { get; set; }
        public int noStart { get; set; }
        public int noEnd { get; set; }
        public int noTotal { get; set; }

        public void OnGet()
        {
            const int itemPerPage = 10;
            int Filter = 1;

            string pageValue = Request.Query["page"];
            int parsedPageIndex;
            int.TryParse(pageValue, out parsedPageIndex);
            pageIndex = parsedPageIndex;

            sKeyword = Request.Query["search"];

            XL_Kho kho = new XL_Kho();
            DSkho = kho.DocDanhSach(sKeyword, Filter);

            if (DSkho.Count > 0)
            {
                pageTotal = (DSkho.Count - 1) / itemPerPage + 1;

                if (pageIndex == 0 || pageIndex > pageTotal)
                {
                    pageIndex = 1;
                }

                noStart = (pageIndex - 1) * itemPerPage + 1;
                noEnd = noStart + itemPerPage - 1;
                noTotal = DSkho.Count;
                if (pageIndex == pageTotal)
                {
                    noEnd = noStart + (DSkho.Count - 1) % itemPerPage;
                }
            }
        }
    }
}

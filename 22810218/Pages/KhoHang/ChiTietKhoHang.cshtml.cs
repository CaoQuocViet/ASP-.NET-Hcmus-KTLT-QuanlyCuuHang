using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class ChiTietKhoHangModel : PageModel
    {
        public Kho[]? DSkho { get; set; }
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

            if (DSkho.Length > 0)
            {
                pageTotal = ((int)DSkho.Length - 1) / itemPerPage + 1;

                if (pageIndex == 0 || pageIndex > pageTotal)
                {
                    pageIndex = 1;
                }

                noStart = (pageIndex - 1) * itemPerPage + 1;
                noEnd = noStart + itemPerPage - 1;
                noTotal = (int)DSkho.Length;
                if (pageIndex == pageTotal)
                {
                    noEnd = noStart + ((int)DSkho.Length - 1) % itemPerPage;
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_ThongKe_TonKhoModel : PageModel
    {
        public Kho[]? DSkho { get; private set; }
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
            int PageIndex = 1; // Declare the PageIndex variable before assigning a value to it
            PageTotal = 1;
            NoStart = 1;
            NoEnd = 1;
            NoTotal = 1;
            SKeyword = string.Empty;
            int Filter = 1;

            string pageValue = Request.Query["page"];
            int.TryParse(pageValue, out int pageIndex);
            PageIndex = pageIndex;

            SKeyword = Request.Query["search"];

            XL_Kho kho = new XL_Kho();
            DSkho = kho.DocDanhSach(SKeyword, Filter);

            if (DSkho.Length > 0)
            {
                PageTotal = ((int)DSkho.Length - 1) / itemPerPage + 1;

                if (PageIndex == 0 || PageIndex > PageTotal)
                {
                    PageIndex = 1;
                }

                NoStart = (PageIndex - 1) * itemPerPage + 1;
                NoEnd = NoStart + itemPerPage - 1;
                NoTotal = (int)DSkho.Length;
                if (PageIndex == PageTotal)
                {
                    NoEnd = NoStart + ((int)DSkho.Length - 1) % itemPerPage;
                }
            }
        }
    }
}

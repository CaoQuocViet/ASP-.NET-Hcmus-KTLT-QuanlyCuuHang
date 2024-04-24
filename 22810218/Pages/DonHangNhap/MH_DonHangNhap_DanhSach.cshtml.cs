using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Pages
{
    public class MH_DonHangNhap_DanhSachModel : PageModel
    {
        public XL_DonNhap XlDonNhap { get; } = new XL_DonNhap();
        public List<DonNhap>? ImportList { get; private set; }
        public int PageIndex { get; private set; }
        public int PageTotal { get; private set; }
        public int NoStart { get; private set; }
        public int NoEnd { get; private set; }
        public int NoTotal { get; private set; }
        public string? SearchKeyword { get; private set; }

        public void OnGet()
        {
            ViewData["Title"] = "Quản lý cửa hàng";
            const int itemPerPage = 10;
            int pageIndex = 1;
            string sKeyword = Request.Query["search"];
            int.TryParse(Request.Query["page"], out pageIndex);

            ImportList = XlDonNhap.DocDanhSach(sKeyword).ToList();
            PageTotal = ImportList.Count > 0 ? ((int)ImportList.Count - 1) / itemPerPage + 1 : 1;
            PageIndex = pageIndex == 0 || pageIndex > PageTotal ? 1 : pageIndex;
            NoStart = (PageIndex - 1) * itemPerPage + 1;
            NoEnd = NoStart + itemPerPage - 1;
            NoTotal = ImportList.Count;
            if (PageIndex == PageTotal)
            {
                NoEnd = NoStart + ((int)ImportList.Count - 1) % itemPerPage;
            }
            SearchKeyword = sKeyword;
        }
    }
}
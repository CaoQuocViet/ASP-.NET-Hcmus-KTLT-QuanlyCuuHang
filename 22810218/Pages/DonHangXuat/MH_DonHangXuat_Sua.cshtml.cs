using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_DonHangXuat_SuaModel : PageModel
    {
        public string? sInfo { get; set; }
        public bool bFlag { get; set; }
        public DonXuat? donxuat { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Quản lý cửa hàng";
            sInfo = string.Empty;
            bFlag = false;
            donxuat = new DonXuat("someMaSo", new DateOnly(), new List<Kho>());
            string donxuatMaSo = Request.Query["id"];
            XL_DonXuat xlDonXuat = new XL_DonXuat(); 
            DonXuat? isDonXuat = xlDonXuat.DocThongTin(donxuatMaSo); 

            if (isDonXuat != null)
            {
                donxuat = isDonXuat;
            }
            else
            {
                sInfo = "Đơn xuất này không tồn tại";
                bFlag = true;
            }
        }

        public void OnPost()
        {
            bFlag = true;
            string sMaSo = Request.Form["id"];
            string sNgay = Request.Form["date"];
            DonXuat tempDonXuat = new DonXuat(sMaSo, new DateOnly(), new List<Kho>());
            XL_DonXuat xlDonXuat = new XL_DonXuat();
            sInfo = xlDonXuat.Sua(sMaSo, sNgay, ref tempDonXuat);
            donxuat = tempDonXuat;
        }
    }
}

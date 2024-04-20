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
        donxuat = new DonXuat("", new DateOnly(), new Kho[0]);
        string donxuatMaSo = Request.Query["id"];
        DonXuat? isDonXuat = new XL_DonXuat().ReadInfo(donxuatMaSo);

        if (isDonXuat != null)
        {
            if (Request.Method == "POST")
            {
                bFlag = true;
                string sMaSo = Request.Form["id"];
                string sNgay = Request.Form["date"];
                XL_DonXuat xlDonXuat = new XL_DonXuat();
                DonXuat temp = donxuat;
                sInfo = xlDonXuat.Sua(sMaSo, sNgay, ref temp);
                donxuat = temp;
            }
        }
    }
}
}

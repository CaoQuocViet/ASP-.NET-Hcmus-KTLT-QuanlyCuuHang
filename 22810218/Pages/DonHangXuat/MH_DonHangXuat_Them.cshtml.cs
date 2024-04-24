using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;
using System.Collections.Generic;

namespace Pages
{
    public class MH_DonHangXuat_ThemModel : PageModel
    {
        public string? sInfoKho { get; set; }
        public string? sInfoDonXuat { get; set; }
        public DonXuat? donxuat { get; set; }
        public Kho? kho { get; set; }
        public List<Kho>? DSkho { get; set; }
        public string? sKhoList { get; set; }
        public int formId { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Quản lý cửa hàng";
            sInfoKho = string.Empty;
            sInfoDonXuat = string.Empty;
            donxuat = new DonXuat("", new DateOnly(), new List<Kho>());
            kho = new Kho();
            DSkho = new List<Kho>();
            sKhoList = string.Empty;
            formId = 0;

            if (Request.Method == "POST")
            {
                sKhoList = Request.Form["khos"];
                XL_Kho xl_Kho = new XL_Kho();
                List<Kho> tempDSkho = DSkho;
                xl_Kho.KiemTraHangHoaTonTai(sKhoList, ref tempDSkho);
                DSkho = tempDSkho;

                string formData = Request.Form["form"];
                if (!string.IsNullOrEmpty(formData))
                {
                    if (int.TryParse(formData, out int parsedFormId))
                    {
                        formId = parsedFormId;
                        string sMaSo = Request.Form["id"];
                        string sNgay = Request.Form["date"];
                        XL_DonXuat xl_DonXuat = new XL_DonXuat();
                        DonXuat tempDonXuat = donxuat;
                        sInfoDonXuat = xl_DonXuat.Them(sMaSo, sNgay, DSkho, ref tempDonXuat);
                        donxuat = tempDonXuat;

                        if (string.IsNullOrEmpty(sInfoDonXuat))
                        {
                            donxuat = new DonXuat("", new DateOnly(), new List<Kho>());
                            kho = new Kho();
                            DSkho = new List<Kho>();
                            sKhoList = string.Empty;
                        }
                    }
                    else if (formId == 2)
                    {
                        string sTenMatHang = Request.Form["pname"];
                        string sSoLuong = Request.Form["quantity"];
                        string sNgaySanXuat = Request.Form["mdate"];
                        string sHanDung = Request.Form["edate"];
                        Kho tempKho = kho;
                        sInfoKho = xl_Kho.XacMinhXuatKho(sTenMatHang, sSoLuong, sNgaySanXuat, sHanDung, ref tempKho);
                        kho = tempKho;
                        if (string.IsNullOrEmpty(sInfoKho))
                        {
                            List<Kho> TempDSkho = DSkho;
                            sInfoKho = xl_Kho.ThemVaoDS(kho, ref TempDSkho);
                            DSkho = TempDSkho;
                            if (string.IsNullOrEmpty(sInfoKho))
                            {
                                sKhoList = xl_Kho.TaoDanhSachHangHoa(DSkho);
                                kho = new Kho();
                            }
                        }
                    }
                }
                List<Kho> xlDSKho = DSkho;
                xl_Kho.KiemTraHangHoaTonTai(sKhoList, ref xlDSKho);
                DSkho = xlDSKho;
            }
        }
    }
}

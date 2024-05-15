using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_DonHangXuat_ThemModel : PageModel
    {
        public string sInfoKho { get; set; } = string.Empty;
        public string sInfoDonXuat { get; set; } = string.Empty;
        public DonXuat donxuat { get; set; } = new DonXuat("", new DateOnly(), new List<Kho>());
        public Kho kho { get; set; } = new Kho("", 0, new DateOnly(), new DateOnly());
        public List<Kho> DSkho { get; set; } = new List<Kho>();
        public string sKhoList { get; set; } = string.Empty;
        public int formId { get; set; } = 0;

        public void OnGet(){}
        public void OnPost()
        {
            sKhoList = Request.Form["khos"];
            var xl_Kho = new XL_Kho();
            List<Kho> tempDSKho = new List<Kho>(DSkho);
            xl_Kho.KiemTraHangHoaTonTai(sKhoList, ref tempDSKho);
            DSkho = tempDSKho;

            int tempFormId;
            if (int.TryParse(Request.Form["form"], out tempFormId))
            {
                formId = tempFormId;
                if (formId == 1)
                {
                    string sMaSo = Request.Form["id"];
                    string sNgay = Request.Form["date"];
                    XL_DonXuat xl_DonXuat = new XL_DonXuat();
                    DonXuat tempDonXuat = donxuat;
                    sInfoDonXuat = xl_DonXuat.Them(sMaSo, sNgay, DSkho, ref tempDonXuat);
                    donxuat = tempDonXuat;

                    if (string.IsNullOrEmpty(sInfoDonXuat))
                    {
                        donxuat = new DonXuat("", new DateOnly(), new List<Kho>());
                        kho = new Kho("", 0, new DateOnly(), new DateOnly());
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
                        tempDSKho = new List<Kho>(DSkho);
                        sInfoKho = xl_Kho.ThemVaoDS(kho, ref tempDSKho);
                        DSkho = tempDSKho;
                        if (string.IsNullOrEmpty(sInfoKho))
                        {
                            sKhoList = xl_Kho.TaoDanhSachHangHoa(DSkho);
                            kho = new Kho("", 0, new DateOnly(), new DateOnly());
                        }
                    }
                }
            }
            tempDSKho = new List<Kho>(DSkho);
            xl_Kho.KiemTraHangHoaTonTai(sKhoList, ref tempDSKho);
            DSkho = tempDSKho;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_DonHangNhap_ThemModel : PageModel
    {
        [BindProperty]
        public string sInfoKho { get; set; } = string.Empty;
        [BindProperty]
        public string sInfoDonNhap { get; set; } = string.Empty;
        [BindProperty]
        public DonNhap import { get; set; } = new DonNhap("", new DateOnly(), new List<Kho>());
        [BindProperty]
        public Kho kho { get; set; } = new Kho("", 0, new DateOnly(), new DateOnly());
        [BindProperty]
        public List<Kho> DSkho { get; set; } = new List<Kho>();
        [BindProperty]
        public string sKhoList { get; set; } = string.Empty;
        [BindProperty]
        public int formId { get; set; } = 0;
        public void OnGet(){}

        public void OnPost()
        {
            sKhoList = Request.Form["khos"];
            var xlKho = new XL_Kho();
            List<Kho> tempDSKho = new List<Kho>(DSkho);
            xlKho.KiemTraHangHoaTonTai(sKhoList, ref tempDSKho);
            DSkho = tempDSKho;

            int tempFormId;
            if (int.TryParse(Request.Form["form"], out tempFormId))
            {
                formId = tempFormId;
                if (formId == 1)
                {
                    string sMaSo = Request.Form["id"];
                    string sNgay = Request.Form["date"];
                    var xlDonNhap = new XL_DonNhap();
                    DonNhap tempImport = import;
                    sInfoDonNhap = xlDonNhap.Them(sMaSo, sNgay, DSkho, ref tempImport);
                    import = tempImport;

                    if (string.IsNullOrEmpty(sInfoDonNhap))
                    {
                        import = new DonNhap(sMaSo, DateOnly.Parse(sNgay), new List<Kho>());
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
                    sInfoKho = xlKho.XacMinhNhapKho(sTenMatHang, sSoLuong, sNgaySanXuat, sHanDung, ref tempKho);
                    kho = tempKho;
                    
                    if (string.IsNullOrEmpty(sInfoKho))
                    {
                        kho.TenMatHang = sTenMatHang;
                        kho.SoLuong = int.Parse(sSoLuong);
                        kho.NgaySanXuat = DateOnly.Parse(sNgaySanXuat);
                        kho.HanDung = DateOnly.Parse(sHanDung);
                        tempDSKho = new List<Kho>(DSkho);
                        sInfoKho = xlKho.ThemVaoDS(kho, ref tempDSKho);
                        DSkho = tempDSKho;
                        
                        if (string.IsNullOrEmpty(sInfoKho))
                        {
                            sKhoList = xlKho.TaoDanhSachHangHoa(DSkho);
                            kho = new Kho(sTenMatHang, int.Parse(sSoLuong), DateOnly.Parse(sNgaySanXuat), DateOnly.Parse(sHanDung));
                        }
                    }
                }
            }
            tempDSKho = new List<Kho>(DSkho);
            xlKho.KiemTraHangHoaTonTai(sKhoList, ref tempDSKho);
            DSkho = tempDSKho;
        }
    }
}

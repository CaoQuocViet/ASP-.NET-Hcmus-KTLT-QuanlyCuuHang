using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Service;

namespace Pages
{
    public class MH_MatHang_ThemModel : PageModel
    {
        [BindProperty]
        public MatHang? Mathang { get; set; }

        public string? SInfo { get; set; }

        public void OnGet()
        {
        }

    }
}

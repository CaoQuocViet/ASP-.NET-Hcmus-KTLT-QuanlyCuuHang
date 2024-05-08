using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Repo
{
    public interface ILT_MatHang : IRepository<MatHang>
    {
        public void LuuDanhSach(List<MatHang> DSmathang);
        public void CapNhatLoaiHang(LoaiHang loaihangOld, LoaiHang loaihangNew);
    }
}

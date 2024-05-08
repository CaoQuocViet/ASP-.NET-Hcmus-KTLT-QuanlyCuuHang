using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repo;
using Entities;

namespace Service
{
    public interface IService<T>
    {
        List<T> DocDanhSach(string sKeyword);
        T DocThongTin(string maSo);
    }
}
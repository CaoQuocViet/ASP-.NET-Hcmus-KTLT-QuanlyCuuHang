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
        T[] DocDanhSach(string sKeyword);
        T? ReadInfo(string maSo);
    }
}
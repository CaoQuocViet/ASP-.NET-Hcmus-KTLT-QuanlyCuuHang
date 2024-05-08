using System.Collections.Generic;
using Entities;

namespace Repo
{
    public interface IRepository<T>
    {
        List<T> DocDanhSach(string sKeyword);
        string Them(T entity);
        string Sua(T entityOld, T entityNew);
        string Xoa(T entity);
        T? DocThongTin(string maSo);
    }
}

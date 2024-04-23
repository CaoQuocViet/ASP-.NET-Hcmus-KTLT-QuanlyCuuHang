using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Repo
{
    public interface IRepository<T>
    {
        T[] DocDanhSach(string sKeyword);
        string Them(T entity);
        string Sua(T entityOld, T entityNew);
        string Xoa(T entity);
        T? ReadInfo(string maSo);
    }
}

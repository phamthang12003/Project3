using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.DataTransferObject;
using E_PROJECT_MANAGER.Models;
using System.Linq.Expressions;

namespace E_PROJECT_MANAGER.Repository
{
    public interface IQuanLyTrangThaiRepository : IBaseRepository<QuanLyTrangThai>
    {
     
    }
    public class QuanLyTrangThaiRepository : BaseRepository<QuanLyTrangThai>, IQuanLyTrangThaiRepository
    {
        public QuanLyTrangThaiRepository(ApplicationDbContext context) : base(context)
        {
        }
    }


}

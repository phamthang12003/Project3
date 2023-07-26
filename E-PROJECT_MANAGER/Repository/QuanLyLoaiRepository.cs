using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.DataTransferObject;
using E_PROJECT_MANAGER.Models;

namespace E_PROJECT_MANAGER.Repository
{
    public interface IQuanLyLoaiRepository : IBaseRepository<QuanLyLoai>
    {
        public ViewDTO<QuanLyLoai> Add(QuanLyLoai entity)
        {
          throw new NotImplementedException();
        }

    }
    public class QuanLyLoaiRepository : BaseRepository<QuanLyLoai>, IQuanLyLoaiRepository
    {
        public QuanLyLoaiRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
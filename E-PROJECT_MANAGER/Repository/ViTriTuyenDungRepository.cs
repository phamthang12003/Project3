using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;

namespace E_PROJECT_MANAGER.Repository
{
    public interface IViTriTuyenDungRepository : IBaseRepository<ViTriTuyenDung>
    {

    }
    public class ViTriTuyenDungRepository : BaseRepository<ViTriTuyenDung>, IViTriTuyenDungRepository
    {
        public ViTriTuyenDungRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

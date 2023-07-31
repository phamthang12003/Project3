using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;

namespace E_PROJECT_MANAGER.Repository
{
    public interface ILichPhongVanRepository : IBaseRepository<LichPhongVan>
    {

    }
    public class LichPhongVanRepository : BaseRepository<LichPhongVan>, ILichPhongVanRepository
    {
        public LichPhongVanRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

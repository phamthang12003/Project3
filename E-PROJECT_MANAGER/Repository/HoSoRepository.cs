using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;

namespace E_PROJECT_MANAGER.Repository
{
    public interface IHoSoRepository : IBaseRepository<HoSo>
    {

    }
    public class HoSoRepository : BaseRepository<HoSo>, IHoSoRepository
    {
        public HoSoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

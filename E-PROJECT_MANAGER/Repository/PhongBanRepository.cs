using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;

namespace E_PROJECT_MANAGER.Repository
{
    public interface IPhongBanRepository : IBaseRepository<PhongBan>
    {

    }
    public class PhongBanRepository : BaseRepository<PhongBan>, IPhongBanRepository
    {
        public PhongBanRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

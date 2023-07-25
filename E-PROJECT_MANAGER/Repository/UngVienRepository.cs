using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;

namespace E_PROJECT_MANAGER.Repository
{
    public interface  IUngVienRepository : IBaseRepository<UngVien>
    {
        
    }
    public class UngVienRepository : BaseRepository<UngVien>, IUngVienRepository
    {
        public UngVienRepository(ApplicationDbContext context) : base(context)
        {
        }
      
    }
}

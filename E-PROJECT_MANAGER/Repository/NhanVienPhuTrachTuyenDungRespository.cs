using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;

namespace E_PROJECT_MANAGER.Repository
{
    public interface INhanVienPhuTrachTuyenDungRepository : IBaseRepository<NhanVienPhuTrachTuyenDung>
    {

    }
    public class NhanVienPhuTrachTuyenDungRespository : BaseRepository<NhanVienPhuTrachTuyenDung>, INhanVienPhuTrachTuyenDungRepository
    {
        public NhanVienPhuTrachTuyenDungRespository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

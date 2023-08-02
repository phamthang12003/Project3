using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.DataTransferObject;
using E_PROJECT_MANAGER.Models;
using E_PROJECT_MANAGER.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace E_PROJECT_MANAGER.Controllers
{
    public class NhanVienPhuTrachTuyenDungController : BaseController<NhanVienPhuTrachTuyenDung>
    {
        private INhanVienPhuTrachTuyenDungRepository _nhanVienPhuTrachTuyenDungRepository;
        
        public ApplicationDbContext _dbContext;
        public NhanVienPhuTrachTuyenDungController(INhanVienPhuTrachTuyenDungRepository nhanVienPhuTrachTuyenDung, ApplicationDbContext dbContext, IBaseRepository<NhanVienPhuTrachTuyenDung> baseRepository, UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor contextAccessor)
                    : base(contextAccessor, userManager, roleManager)
        {
            _nhanVienPhuTrachTuyenDungRepository = nhanVienPhuTrachTuyenDung;
            _dbContext = dbContext;
        }

        public IActionResult ResposeDataTables(DataTableAjaxPostModel postModel)
        {
            //Kiem tra search
            var search = "";
            if (postModel.search != null)
            {
                search = postModel.search.value;
            }

            //Kiem tra sap xep
            var columnName = "Id";
            var columnAsc = false;

            if (postModel.order != null)
            {
                columnName = postModel.columns[postModel.order[0].column].name;
                if (postModel.order[0].dir.Equals("asc"))
                {
                    columnAsc = true;
                }
                if (postModel.order[0].dir.Equals("desc"))
                {
                    columnAsc = false;
                }
            }
            var start = postModel.start;
            var length = postModel.length;
            //Include

            Expression<Func<NhanVienPhuTrachTuyenDung, object>>[] includeProperties = new Expression<Func<NhanVienPhuTrachTuyenDung, object>>[] {
                        x => x.GetViTriTuyenDung
                    };

            var result = _nhanVienPhuTrachTuyenDungRepository.Filter(
                r => (string.IsNullOrEmpty(search)) || (
                    (!string.IsNullOrEmpty(search)) && (
                        r.NhanVienId.ToString().ToLower().Contains(search.ToLower()) 
                      
                    )
                ),
                columnName,
                columnAsc,
                start,
                length,
                postModel.draw,
                includeProperties
                );  
            var data = result.data;
            var listUser = _userManager.Users.ToList();
            var query = from rd in data
                        join us in listUser on rd.NhanVienId equals us.Id
                        select new NhanVienPhuTrachTuyenDung()
                        {
                            Id = rd.Id,
                            ViTriTuyenDungId = rd.ViTriTuyenDungId,
                            NhanVienId = us.FullName,
                            GetViTriTuyenDung = rd.GetViTriTuyenDung
                        };
            result.data = query.ToList();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult ViewCreateOrUpdate(int id)
        {
            ViewBag.ViTriTuyenDungId = new SelectList(_dbContext.ViTriTuyenDungs.ToList(), "Id", "TenViTriTuyenDung");
            var listUser = _userManager.Users.ToList();
            ViewBag.ListUser = new SelectList(listUser, "Id", "FullName");
            var model = new NhanVienPhuTrachTuyenDung();


            if (id > 0)
            {
                model = _nhanVienPhuTrachTuyenDungRepository.GetById(id);

            }

            return PartialView(model);
        }


        [HttpPost]
        public IActionResult Save(NhanVienPhuTrachTuyenDung entity)
        {

            var result = _nhanVienPhuTrachTuyenDungRepository.Save(entity.Id, entity);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult DeleteItem(int id)
        {
            if (id > 0)
            {
                var deteledItem = _nhanVienPhuTrachTuyenDungRepository.GetById(id);
                if (deteledItem != null)
                {
                    var result = _nhanVienPhuTrachTuyenDungRepository.Delelte(deteledItem);
                    return Ok(result);
                }
            }
            return BadRequest();

        }
    }
}

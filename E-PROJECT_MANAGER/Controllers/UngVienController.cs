using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;
using E_PROJECT_MANAGER.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_PROJECT_MANAGER.Controllers
{
    [Authorize]
    public class UngVienController : BaseController<UngVien>
    {
        private IUngVienRepository _ungVienRepository;

        public UngVienController(IUngVienRepository ungVienRepository, ApplicationDbContext applicationDbContext, IBaseRepository<UngVien> baseRepository)
                    : base(baseRepository, applicationDbContext)
        {
            _ungVienRepository = ungVienRepository;
        }

       

        [HttpGet]
        [Authorize(Roles = "ADMIN, LETAN")]
        
        public IActionResult ViewCreateOrUpdate(int id)
        {
            var model = new UngVien();
            

            if (id > 0)
            {
                model = _ungVienRepository.GetById(id);

            }

            return PartialView(model);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Save(UngVien entity)
        {
            var result = _ungVienRepository.Save(entity.Id, entity);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult DeleteItem(int id)
        {
            if(id > 0)
            {
                var deteledItem = _ungVienRepository.GetById(id);
                if(deteledItem != null)
                {
                    var result = _ungVienRepository.Delelte(deteledItem);
                    return Ok(result);
                }
            }
            return BadRequest();    
            
        }
    }
}

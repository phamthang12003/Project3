using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;
using E_PROJECT_MANAGER.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_PROJECT_MANAGER.Controllers
{
    public class PhongBanController : BaseController<PhongBan>
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private IPhongBanRepository _phongBanRepository;
        public PhongBanController(IPhongBanRepository phongBanRepository, ApplicationDbContext applicationDbContext, IBaseRepository<PhongBan> baseRepository)
                   : base(baseRepository, applicationDbContext)
        {
            _phongBanRepository = phongBanRepository;
        }

        [HttpGet]
        public IActionResult ViewCreateOrUpdate(int id)
        {
            var model = new PhongBan();


            if (id > 0)
            {
                model = _phongBanRepository.GetById(id);

            }

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Save(PhongBan entity)
        {
            var result = _phongBanRepository.Save(entity.Id, entity);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult DeleteItem(int id)
        {
            if (id > 0)
            {
                var deteledItem = _phongBanRepository.GetById(id);
                if (deteledItem != null)
                {
                    var result = _phongBanRepository.Delelte(deteledItem);
                    return Ok(result);
                }
            }
            return BadRequest();

        }
    }
}

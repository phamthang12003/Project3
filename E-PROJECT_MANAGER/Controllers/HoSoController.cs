using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;
using E_PROJECT_MANAGER.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_PROJECT_MANAGER.Controllers
{
    public class HoSoController : BaseController<HoSo>
    {

        private IHoSoRepository _hoSoRepository;

        public HoSoController(IHoSoRepository hoSoRepository, ApplicationDbContext applicationDbContext, IBaseRepository<HoSo> baseRepository)
                    : base(baseRepository, applicationDbContext)
        {
            _hoSoRepository = hoSoRepository;
        }

        [HttpGet]
        public IActionResult ViewCreateOrUpdate(int id)
        {
            var model = new HoSo();


            if (id > 0)
            {
                model = _hoSoRepository.GetById(id);

            }

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Save(HoSo entity)
        {
            var result = _hoSoRepository.Save(entity.Id.Value , entity);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult DeleteItem(int id)
        {
            if (id > 0)
            {
                var deteledItem = _hoSoRepository.GetById(id);
                if (deteledItem != null)
                {
                    var result = _hoSoRepository.Delelte(deteledItem);
                    return Ok(result);
                }
            }
            return BadRequest();

        }


        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}

using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;
using E_PROJECT_MANAGER.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_PROJECT_MANAGER.Controllers
{
    public class QuanLyLoaiController : BaseController<QuanLyLoai>
    {
        private IQuanLyLoaiRepository _quanLyLoaiRepository;

        public QuanLyLoaiController(IQuanLyLoaiRepository quanLyLoaiRepository, ApplicationDbContext applicationDbContext, IBaseRepository<QuanLyLoai> baseRepository)
                    : base(baseRepository, applicationDbContext)
        {
            _quanLyLoaiRepository = quanLyLoaiRepository;
        }

        [HttpGet]
        public IActionResult ViewCreateOrUpdate(int id)
        {
            var model = new QuanLyLoai();


            if (id > 0)
            {
                model = _quanLyLoaiRepository.GetById(id);

            }

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Save(QuanLyLoai entity)
        {
            var result = _quanLyLoaiRepository.Save(entity.Id, entity);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult DeleteItem(int id)
        {
            if (id > 0)
            {
                var deteledItem = _quanLyLoaiRepository.GetById(id);
                if (deteledItem != null)
                {
                    var result = _quanLyLoaiRepository.Delelte(deteledItem);
                    return Ok(result);
                }
            }
            return BadRequest();

        }
        [HttpPost]
        public IActionResult Update(QuanLyLoai entity)
        {
            if (ModelState.IsValid)
            {
                var existingEntity = _quanLyLoaiRepository.GetById(entity.Id);
                if (existingEntity != null)
                {
                    // Update properties of the existingEntity with values from the entity received
                    existingEntity.TenLoai = entity.TenLoai;
                    existingEntity.GiaTri = entity.GiaTri;
                    existingEntity.CSSCLass = entity.CSSCLass;

                    existingEntity.SapXep = entity.SapXep;




                    // ... and so on for other properties

                    var result = _quanLyLoaiRepository.Save(existingEntity.Id, existingEntity);
                    return Ok(result);
                }
            }
            return BadRequest();
        }



    }
}


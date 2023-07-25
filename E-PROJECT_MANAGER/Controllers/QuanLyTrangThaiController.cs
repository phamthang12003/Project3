

using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;
using E_PROJECT_MANAGER.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_PROJECT_MANAGER.Controllers
{
    public class QuanLyTrangThaiController : BaseController<QuanLyTrangThai>
    {
        private IQuanLyTrangThaiRepository _quanLyTrangThaiRepository;

        public QuanLyTrangThaiController(IQuanLyTrangThaiRepository quanLyTrangThaiRepository, ApplicationDbContext applicationDbContext, IBaseRepository<QuanLyTrangThai> baseRepository)
            : base(baseRepository, applicationDbContext)
        {
            _quanLyTrangThaiRepository = quanLyTrangThaiRepository;
        }

        [HttpGet]
        public IActionResult ViewCreateOrUpdate(int id)
        {
            var model = new QuanLyTrangThai();


            if (id > 0)
            {
                model = _quanLyTrangThaiRepository.GetById(id);

            }

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Save(QuanLyTrangThai entity)
        {
            var result = _quanLyTrangThaiRepository.Save(entity.Id, entity);
            return Ok(result);
        }

        

        [HttpPost]
        public IActionResult Update(QuanLyTrangThai entity)
        {
            if (ModelState.IsValid)
            {
                var existingEntity = _quanLyTrangThaiRepository.GetById(entity.Id);
                if (existingEntity != null)
                {
                    // Update properties of the existingEntity with values from the entity received
                    existingEntity.TenBaang = entity.TenBaang;
                    existingEntity.TenTrangThai = entity.TenTrangThai;
                    existingEntity.GiaTri = entity.GiaTri;

                    existingEntity.CSSClass = entity.CSSClass;
                    existingEntity.SapXep = entity.SapXep;

                    // ... and so on for other properties

                    var result = _quanLyTrangThaiRepository.Save(existingEntity.Id, existingEntity);
                    return Ok(result);
                }
            }
            return BadRequest();
        }
        [HttpGet]
        public IActionResult DeleteItem(int id)
        {
            if (id > 0)
            {
                var deteledItem = _quanLyTrangThaiRepository.GetById(id);
                if (deteledItem != null)
                {
                    var result = _quanLyTrangThaiRepository.Delelte(deteledItem);
                    return Ok(result);
                }
            }
            return BadRequest();

        }
    }
}


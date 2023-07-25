using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;
using E_PROJECT_MANAGER.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_PROJECT_MANAGER.Controllers
{
    public class UngVienController : BaseController<UngVien>
    {
        private IUngVienRepository _ungVienRepository;

        public UngVienController(IUngVienRepository ungVienRepository, ApplicationDbContext applicationDbContext, IBaseRepository<UngVien> baseRepository)
                    : base(baseRepository, applicationDbContext)
        {
            _ungVienRepository = ungVienRepository;
        }

       

        [HttpGet]
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
        public IActionResult Save(UngVien entity)
        {
            var result = _ungVienRepository.Save(entity.Id, entity);
            return Ok(result);
        }

        [HttpGet]
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
        //[HttpPost]
        //public IActionResult Update(UngVien entity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var existingEntity = _ungVienRepository.GetById(entity.Id);
        //        if (existingEntity != null)
        //        {
        //            // Update properties of the existingEntity with values from the entity received
        //            existingEntity.GioiTinh = entity.GioiTinh;
        //            existingEntity.Tuoi = entity.Tuoi;
        //            existingEntity.TenUngVien = entity.TenUngVien;
        //            existingEntity.DiaChi = entity.DiaChi;
        //            existingEntity.ViTriUngTuyen = entity.ViTriUngTuyen;
        //            existingEntity.KinhNghiemLamViec = entity.KinhNghiemLamViec;

        //            // ... and so on for other properties

        //            var result = _ungVienRepository.Save(existingEntity);
        //            return Ok(result);
        //        }
        //    }
        //    return BadRequest();
        //}
    }
}

using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.DataTransferObject;
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

			//Goi vao Repository va dien cac tham so phu hop
			var result = _phongBanRepository.Filter(
				r => (string.IsNullOrEmpty(search)) || (
					(!string.IsNullOrEmpty(search)) && (
						r.TenPhongBan.ToLower().Contains(search.ToLower())
					)
				),
				columnName,
				columnAsc,
				start,
				length,
				postModel.draw
				);
			return Ok(result);

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
            var result = _phongBanRepository.Save(entity.Id.Value, entity);
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
        //[HttpPost]
        //public IActionResult Update(PhongBan entity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var existingEntity = _phongBanRepository.GetById(entity.Id);
        //        if (existingEntity != null)
        //        {
        //            // Update properties of the existingEntity with values from the entity received
        //            existingEntity.TenPhongBan = entity.TenPhongBan;
        //            // ... and so on for other properties

        //            var result = _phongBanRepository.Save(existingEntity);
        //            return Ok(result);
        //        }
        //    }
        //    return BadRequest();
        //}
    }
}

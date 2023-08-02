using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.DataTransferObject;
using E_PROJECT_MANAGER.Models;
using E_PROJECT_MANAGER.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace E_PROJECT_MANAGER.Controllers
{
	public class ViTriTuyenDungController : BaseController<ViTriTuyenDung>
	{

		private IViTriTuyenDungRepository _viTriTuyenDungRepository;
		public ApplicationDbContext _dbContext;

		public ViTriTuyenDungController(IViTriTuyenDungRepository viTriTuyenDungRepository, ApplicationDbContext dbContext, IBaseRepository<ViTriTuyenDung> baseRepository)
					: base(baseRepository, dbContext)
		{
			_viTriTuyenDungRepository = viTriTuyenDungRepository;
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

            Expression<Func<ViTriTuyenDung, object>>[] includeProperties = new Expression<Func<ViTriTuyenDung, object>>[] {
						x => x.GetPhongBan
					};

            //Goi vao Repository va dien cac tham so phu hop
            var result = _viTriTuyenDungRepository.Filter(
				r => (string.IsNullOrEmpty(search)) || (
					(!string.IsNullOrEmpty(search)) && (
						r.TenViTriTuyenDung.ToLower().Contains(search.ToLower()) ||
						r.Request.ToLower().Contains(search.ToLower()) ||
						r.Title.ToLower().Contains(search.ToLower())
					)
				),
				columnName,
				columnAsc,
				start,
				length,
				postModel.draw,
				includeProperties
				);
			return Ok(result);
		}

		[HttpGet]
		public IActionResult ViewCreateOrUpdate(int id)
		{
            ViewBag.PhongBanId = new SelectList(_dbContext.PhongBans.ToList(), "Id", "TenPhongBan");
            var model = new ViTriTuyenDung();


			if (id > 0)
			{
				model = _viTriTuyenDungRepository.GetById(id);

			}

			return PartialView(model);
		}

		[HttpPost]
		public IActionResult Save(ViTriTuyenDung entity)
		{

			var result = _viTriTuyenDungRepository.Save(entity.Id, entity);
			return Ok(result);
		}

		[HttpGet]
		public IActionResult DeleteItem(int id)
		{
			if (id > 0)
			{
				var deteledItem = _viTriTuyenDungRepository.GetById(id);
				if (deteledItem != null)
				{
					var result = _viTriTuyenDungRepository.Delelte(deteledItem);
					return Ok(result);
				}
			}
			return BadRequest();

		}


		//public IActionResult Index()
		//{
		//	return View();
		//}
	}
}

using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.DataTransferObject;
using E_PROJECT_MANAGER.Models;
using E_PROJECT_MANAGER.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_PROJECT_MANAGER.Controllers
{
    public class HoSoController : BaseController<HoSo>
    {

        private IHoSoRepository _hoSoRepository;
        public ApplicationDbContext _dbContext;

        public HoSoController(IHoSoRepository hoSoRepository, ApplicationDbContext dbContext, IBaseRepository<HoSo> baseRepository)
                    : base(baseRepository, dbContext)
        {
            _hoSoRepository = hoSoRepository;
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


            Expression<Func<HoSo, object>>[] includeProperties = new Expression<Func<HoSo, object>>[] {
                        x => x.GetUngVien
                    };
            //Goi vao Repository va dien cac tham so phu hop
            var result = _hoSoRepository.Filter(
				r => (string.IsNullOrEmpty(search)) || (
					(!string.IsNullOrEmpty(search)) && (
						r.LoaiHoSo.ToLower().Contains(search.ToLower()) ||
						r.LinkHoSo.ToLower().Contains(search.ToLower())
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
            ViewBag.UngVienId = new SelectList(_dbContext.UngViens.ToList(), "Id", "TenUngVien");
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
            var result = _hoSoRepository.Save(entity.Id , entity);
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

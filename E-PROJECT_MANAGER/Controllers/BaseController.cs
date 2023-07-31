using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_PROJECT_MANAGER.Controllers
{
    public class BaseController<T> : Controller where T : class
    {
        private IBaseRepository<T> _baseRepository;
        protected ApplicationDbContext _context;
        public BaseController(IBaseRepository<T> baseRepository, ApplicationDbContext context)
        {
            _baseRepository = baseRepository;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public virtual IActionResult Filter(int index = 1, int size = 10, int draw = 1)
        {
            var result = _baseRepository.Filter(null, "Id", false, (index - 1) * size, size, draw);
            return Ok(result);
        }

        

        public IActionResult GetById(int id)
        {
            var result = _baseRepository.GetById(id);
            return Ok(result);
        }
        public IActionResult Insert(T entity)
        {
            var result = _baseRepository.Insert(entity);
            return Ok(result);
        }
        public IActionResult Update(T entity)
        {
            var result = _baseRepository.Update(entity);
            return Ok(result);
        }
        public IActionResult Delete(T entity)
        {
            var result = _baseRepository.Delelte(entity);
            return Ok(result);
        }
    }
}

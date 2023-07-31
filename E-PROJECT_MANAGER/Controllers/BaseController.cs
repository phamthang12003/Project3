using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;
using E_PROJECT_MANAGER.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_PROJECT_MANAGER.Controllers
{
    public class BaseController<T> : Controller where T : class
    {
        protected IBaseRepository<T> _baseRepository;
        protected ApplicationDbContext _context;

        protected readonly IHttpContextAccessor _contextAccessor;
        protected readonly UserManager<CustomUser> _userManager;
        protected readonly RoleManager<IdentityRole> _roleManager;

        public BaseController(IHttpContextAccessor contextAccessor,UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager)
        {   
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected async Task<CustomUser> GetCurrentUserAsyc()   
        {
            var currentUser = _contextAccessor.HttpContext.User;
            var ht = "";
            var userId = "";
            if (currentUser.Identity.IsAuthenticated)
            {
                ht = currentUser.Identity.Name; //lấy ra user
                var userTimThay = await _userManager.FindByNameAsync(currentUser.Identity.Name);
                return userTimThay;
            }
            return null;
            //ViewBag.Ht = ht;
            //return View();
        }
        public BaseController(IBaseRepository<T> baseRepository, ApplicationDbContext context)
        {
            _baseRepository = baseRepository;
            _context = context;
        }
        public virtual IActionResult Index()
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

using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_PROJECT_MANAGER.Controllers
{
    public class HomeController : BaseController<object>
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(IHttpContextAccessor contextAccessor, UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager) : base(contextAccessor, userManager, roleManager)
        {
        }

        public async Task<IActionResult> SeedingRoleAsync()
        {
            var dbSeedRole = new DbSeedRole(_roleManager);
            await dbSeedRole.RoleData();
            return Ok("seed role thanh cong!");
        }

        public  async Task<IActionResult> Index1()
        {
            var curretUser = await GetCurrentUserAsyc();
            if (curretUser != null) 
            {
                ViewBag.HT = curretUser.UserName;
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
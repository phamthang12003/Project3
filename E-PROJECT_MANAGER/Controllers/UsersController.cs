using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;
using E_PROJECT_MANAGER.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_PROJECT_MANAGER.Controllers
{
    public class UsersController : Controller
    {

        protected ApplicationDbContext _context;

        protected readonly IHttpContextAccessor _contextAccessor;
        protected readonly UserManager<CustomUser> _userManager;
        protected readonly RoleManager<IdentityRole> _roleManager;
        public UsersController(IHttpContextAccessor contextAccessor, UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _roleManager = roleManager;
        }

        public  IActionResult Index()
        {
            var dsUser = _userManager.Users.ToList();
            return View(dsUser);
        }

        public async Task<IActionResult> ViewUpdate(string id)
        {
            var userCanTim = await _userManager.FindByIdAsync(id);
            return PartialView(userCanTim);
        }
        [HttpPost]
        public async Task<IActionResult> doUpdate([Bind("Id, PhoneNumber, FullName, Age")] CustomUser user)
        {
            if (user != null)
            {
                var userCanUpdate = await _userManager.FindByIdAsync(user.Id);
                if (userCanUpdate != null)
                {
                    userCanUpdate.FullName = user.FullName;
                    userCanUpdate.Age = user.Age;
                    userCanUpdate.PhoneNumber = user.PhoneNumber;

                    await _userManager.UpdateAsync(userCanUpdate);
                    TempData["thanhCong"] = "cap nhap thanh cong!";
                    return Redirect("/Users/Index ");
                }
            }
            return BadRequest();
        }

        public async Task<IActionResult> viewSetRoles(string id)
        {
            var dsAllQuyen = await _roleManager.Roles.ToListAsync();
            var userDuocChon = await _userManager.FindByIdAsync(id);
            if(userDuocChon != null)
            {
                var userRoles = await _userManager.GetRolesAsync(userDuocChon);
                ViewBag.userRole = userRoles.ToList();
                ViewBag.userId = id;
                return PartialView(dsAllQuyen);
            }
            return BadRequest();
            
        }
        [HttpPost]
        public async Task<IActionResult> doSetRoles(string userid, List<string> arrRole)
        {
            var userDuocChon = await _userManager.FindByIdAsync(userid);
            var dsAllQuyen = await _roleManager.Roles.ToListAsync();
            var arrStringQuyen = dsAllQuyen.Select(r => r.Name);
            if (userDuocChon != null)
            {
                //xóa tat ca role cua user 
                await _userManager.RemoveFromRolesAsync(userDuocChon, arrStringQuyen);
                //thêm lại role mới
                await _userManager.AddToRolesAsync(userDuocChon, arrRole);
                TempData["thanhCong"] = "cập nhật thành công!";
                return Redirect("/Users/Index ");
            }
            return BadRequest();
        }
    }
}

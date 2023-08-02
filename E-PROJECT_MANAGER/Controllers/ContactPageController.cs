using Microsoft.AspNetCore.Mvc;

namespace E_PROJECT_MANAGER.Controllers
{
    public class ContactPageController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
    }
}

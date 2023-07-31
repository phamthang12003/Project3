using Microsoft.AspNetCore.Mvc;

namespace E_PROJECT_MANAGER.ViewComponents
{
    public class UploadSingleFile : ViewComponent
    {
        public IViewComponentResult Invoke(string inputTargetSelector, string fileAddress)
        {
            ViewBag.InputTargetSelector = inputTargetSelector;
            ViewBag.FileAddress = fileAddress;
            return View();
        }
    }
}

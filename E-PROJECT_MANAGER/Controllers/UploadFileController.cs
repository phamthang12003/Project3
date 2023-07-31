using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_PROJECT_MANAGER.Controllers
{
    public class UploadFileController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment _webHostEnvironment;
        public UploadFileController(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        public IActionResult UploadSingleFile(IFormFile ifile)
        {
            var totalFile = Request.Form.Files.Count();
            if(totalFile > 0)
            {
                var file = Request.Form.Files[0];
                var fileInfo = new FileInfo(file.FileName);
                var fileName = fileInfo.Name;
                var fileExtension = fileInfo.Extension;
                var fileSize = file.Length;

                var acceptExtension = new List<string>() { ".pdf", ".doc", "png", ".jpg"};
                var acceptSize = 10 * 1024 * 1024;
                if (acceptExtension.Contains(fileExtension))
                {
                    if(fileSize <= acceptSize) {
                        var wwwRootFolder = _webHostEnvironment.WebRootPath;
                        var uploadFolder = Path.Combine(wwwRootFolder, "upload");
                        if(!Directory.Exists(uploadFolder))
                        {
                            Directory.CreateDirectory(uploadFolder);
                        }
                        var dateNowToString = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
                        var fileNameFull = dateNowToString + "_" + fileName;
                        var uploadDir = Path.Combine(uploadFolder, fileNameFull);
                        using(var stream = new FileStream(uploadDir, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        return Ok(fileNameFull);
                    }
                    else
                    {
                        return BadRequest("File size to large. Please upload file small than 10MB");
                    }
                }
                else
                {
                    return BadRequest("File Ext not in accept lisr!!");
                }
            }
            return BadRequest("No File exits");
        }
    }
}

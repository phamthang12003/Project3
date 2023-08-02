using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.DataTransferObject;
using E_PROJECT_MANAGER.Models;
using E_PROJECT_MANAGER.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace E_PROJECT_MANAGER.Controllers
{
    [Authorize]
    public class UngVienController : BaseController<UngVien>
    {
        private IUngVienRepository _ungVienRepository;

        public UngVienController(IUngVienRepository ungVienRepository, ApplicationDbContext applicationDbContext, IBaseRepository<UngVien> baseRepository)
                    : base(baseRepository, applicationDbContext)
        {
            _ungVienRepository = ungVienRepository;
        }

       

        [HttpGet]
        [Authorize(Roles = "ADMIN, LETAN")]
        
        public IActionResult ViewCreateOrUpdate(int id)
        {
            var model = new UngVien();
            

            if (id > 0)
            {
                model = _ungVienRepository.GetById(id);

            }

            return PartialView(model);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Save(UngVien entity)
        {
            var result = _ungVienRepository.Save(entity.Id.Value, entity);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult DeleteItem(int id)
        {
            if(id > 0)
            {
                var deteledItem = _ungVienRepository.GetById(id);
                if(deteledItem != null)
                {
                    var result = _ungVienRepository.Delelte(deteledItem);
                    return Ok(result);
                }
            }
            return BadRequest();    
            
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
            var result = _ungVienRepository.Filter(
                r => (string.IsNullOrEmpty(search)) || (
                    (!string.IsNullOrEmpty(search)) && (
                        r.TenUngVien.ToLower().Contains(search.ToLower())
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


		[HttpPost]
        public IActionResult AddFullDetail(RequestUngVienFullDetail formData)
        {
            if (formData != null)
            {
                var ungVien = new UngVien()
                {
                    TenUngVien = formData.fullName,
                    Email = formData.email,
                    phoneNumber = formData.phoneNumber,
                    ViTriUngTuyen = formData.jobSector,
                    KinhNghiemLamViec = formData.commentMessage,
                    Tuoi = formData.Age,
                    GioiTinh = formData.Sex,
                    DiaChi = formData.Address
                };
                _context.UngViens.Add(ungVien);
                _context.SaveChanges();

                var hs = new HoSo();
                hs.UngVienId = formData.Id;
                hs.LoaiHoSo = "CV";
                hs.LinkHoSo = formData.urlFile;
                _context.HoSos.Add(hs);
                _context.SaveChanges();

                return Ok(formData);
            }
            return BadRequest();
        }

        public IActionResult ExportToExcel()
        {
            var result = _ungVienRepository.GetAll();
            if(result != null)
            {
                var stream = new MemoryStream();
                var data = result.data;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using(var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("UngVien");
                    worksheet.Cells[1, 1].Value = "Id";
                    worksheet.Cells[1, 2].Value = "Giới tính"; 
                    worksheet.Cells[1, 3].Value = "Tuổi";
                    worksheet.Cells[1, 4].Value = "Tên ứng viên";
                    worksheet.Cells[1, 5].Value = "Địa chỉ";
                    worksheet.Cells[1, 6].Value = "Vị trí ứng tuyển";
                    worksheet.Cells[1, 7].Value = "Kinh nghiệm làm việc";

                    for (var i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = data[i].Id;
                        worksheet.Cells[i + 2, 2].Value = data[i].GioiTinh;
                        worksheet.Cells[i + 2, 3].Value = data[i].Tuoi;
                        worksheet.Cells[i + 2, 4].Value = data[i].TenUngVien;
                        worksheet.Cells[i + 2, 5].Value = data[i].DiaChi;
                        worksheet.Cells[i + 2, 6].Value = data[i].ViTriUngTuyen;
                        worksheet.Cells[i + 2, 7].Value = data[i].KinhNghiemLamViec;

                    }
                    worksheet.Cells.AutoFitColumns();
                    package.SaveAs(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                }
                var fileDownloadName = "ungVienExcel.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return new FileStreamResult(stream, contentType)
                {
                    FileDownloadName = fileDownloadName

                };

            }

            return null;
        }

    }
}

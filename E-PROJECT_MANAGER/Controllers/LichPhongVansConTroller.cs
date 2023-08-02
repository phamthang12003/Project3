using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;

namespace E_PROJECT_MANAGER.Controllers
{
    public class LichPhongVansController : Controller
    {
        private readonly ApplicationDbContext _context;



        public LichPhongVansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LichPhongVans
        public async Task<IActionResult> Index()
        {
            var viTriTuyenDung = _context.ViTriTuyenDungs.ToList();
            ViewBag.ViTriTuyenDung = viTriTuyenDung;
            return _context.LichPhongVans != null ?
                          View(await _context.LichPhongVans.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.LichPhongVans'  is null.");


        }

        public IActionResult FindAllEvent()
        {
            var events = _context.LichPhongVans.Select(l => new
            {
                id = l.Id,
                title = string.Format("Lịch phỏng vấn \nngày {0}", l.NgayPhongVan.Value.ToString("dd/MM")),
                start = l.ThoiGianBatDau,
                end = l.ThoiGianKetThuc
            }).ToList();
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> Save(LichPhongVan lpv)
        {
            if (lpv != null)
            {
                if (lpv.Id == 0)
                {

                    _context.LichPhongVans.Add(lpv);
                    _context.SaveChanges();
                    return Ok("Them Lich thanh cong!");
                }
                if (lpv.Id > 0)
                {
                    _context.LichPhongVans.Update(lpv);
                    _context.SaveChanges();
                    return Ok("Cap nhat thanh cong!");

                }
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrUpdateView(int id)
        {
            var lichPhongVan = await _context.LichPhongVans.FindAsync(id);
            ViewBag.LichPhongVan = lichPhongVan;
            return PartialView(id);
        }

        [HttpPost]
        public IActionResult UpdateEvent(int id, DateTime start, DateTime end)
        {
            var lichPhongVan = _context.LichPhongVans.FirstOrDefault(l => l.Id == id);
            if (lichPhongVan == null)
            {
                return NotFound();
            }

            TimeSpan duration = end - start;
            lichPhongVan.NgayPhongVan = start.Date;
            lichPhongVan.ThoiGianBatDau = start;
            lichPhongVan.ThoiGianKetThuc = start.Add(duration);

            _context.SaveChanges();

            return Ok();
        }

        // GET: LichPhongVans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LichPhongVans == null)
            {
                return NotFound();
            }

            var lichPhongVan = await _context.LichPhongVans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lichPhongVan == null)
            {
                return NotFound();
            }

            return View(lichPhongVan);
        }

        // GET: LichPhongVans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LichPhongVans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NgayPhongVan,ThoiGianBatDau,ThoiGianKetThuc,ViTriTuyenDungId,UngVienId,Id,LoaiId,TrangThaiId,IsDelete,NgayXoa")] LichPhongVan lichPhongVan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lichPhongVan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lichPhongVan);
        }



        // GET: LichPhongVans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LichPhongVans == null)
            {
                return NotFound();
            }

            var lichPhongVan = await _context.LichPhongVans.FindAsync(id);
            if (lichPhongVan == null)
            {
                return NotFound();
            }
            return View(lichPhongVan);
        }

        // POST: LichPhongVans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NgayPhongVan,ThoiGianBatDau,ThoiGianKetThuc,ViTriTuyenDungId,UngVienId,Id,LoaiId,TrangThaiId,IsDelete,NgayXoa")] LichPhongVan lichPhongVan)
        {
            if (id != lichPhongVan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lichPhongVan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LichPhongVanExists(lichPhongVan.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lichPhongVan);
        }

        // GET: LichPhongVans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LichPhongVans == null)
            {
                return NotFound();
            }

            var lichPhongVan = await _context.LichPhongVans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lichPhongVan == null)
            {
                return NotFound();
            }

            return View(lichPhongVan);
        }

        // POST: LichPhongVans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LichPhongVans == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LichPhongVans'  is null.");
            }
            var lichPhongVan = await _context.LichPhongVans.FindAsync(id);
            if (lichPhongVan != null)
            {
                _context.LichPhongVans.Remove(lichPhongVan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LichPhongVanExists(int id)
        {
            return (_context.LichPhongVans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

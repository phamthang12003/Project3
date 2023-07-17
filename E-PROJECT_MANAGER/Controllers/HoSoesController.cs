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
    public class HoSoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HoSoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HoSoes
        public async Task<IActionResult> Index()
        {
              return _context.HoSos != null ? 
                          View(await _context.HoSos.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.HoSos'  is null.");
        }

        // GET: HoSoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoSos == null)
            {
                return NotFound();
            }

            var hoSo = await _context.HoSos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoSo == null)
            {
                return NotFound();
            }

            return View(hoSo);
        }

        // GET: HoSoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HoSoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UngVienId,LoaiHoSo,LinkHoSo,Id,LoaiId,TrangThaiId,IsDelete,NgayXoa")] HoSo hoSo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoSo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hoSo);
        }

        // GET: HoSoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoSos == null)
            {
                return NotFound();
            }

            var hoSo = await _context.HoSos.FindAsync(id);
            if (hoSo == null)
            {
                return NotFound();
            }
            return View(hoSo);
        }

        // POST: HoSoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UngVienId,LoaiHoSo,LinkHoSo,Id,LoaiId,TrangThaiId,IsDelete,NgayXoa")] HoSo hoSo)
        {
            if (id != hoSo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoSo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoSoExists(hoSo.Id))
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
            return View(hoSo);
        }

        // GET: HoSoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HoSos == null)
            {
                return NotFound();
            }

            var hoSo = await _context.HoSos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoSo == null)
            {
                return NotFound();
            }

            return View(hoSo);
        }

        // POST: HoSoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HoSos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HoSos'  is null.");
            }
            var hoSo = await _context.HoSos.FindAsync(id);
            if (hoSo != null)
            {
                _context.HoSos.Remove(hoSo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoSoExists(int id)
        {
          return (_context.HoSos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

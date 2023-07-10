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
    public class ViTriTuyenDungsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ViTriTuyenDungsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ViTriTuyenDungs
        public async Task<IActionResult> Index()
        {
              return _context.ViTriTuyenDungs != null ? 
                          View(await _context.ViTriTuyenDungs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ViTriTuyenDungs'  is null.");
        }

        // GET: ViTriTuyenDungs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ViTriTuyenDungs == null)
            {
                return NotFound();
            }

            var viTriTuyenDung = await _context.ViTriTuyenDungs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viTriTuyenDung == null)
            {
                return NotFound();
            }

            return View(viTriTuyenDung);
        }

        // GET: ViTriTuyenDungs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ViTriTuyenDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhongBanID,TenViTriTuyenDung,Title,Request,Number,Id,LoaiId,TrangThaiId,IsDelete,NgayXoa")] ViTriTuyenDung viTriTuyenDung)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viTriTuyenDung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viTriTuyenDung);
        }

        // GET: ViTriTuyenDungs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ViTriTuyenDungs == null)
            {
                return NotFound();
            }

            var viTriTuyenDung = await _context.ViTriTuyenDungs.FindAsync(id);
            if (viTriTuyenDung == null)
            {
                return NotFound();
            }
            return View(viTriTuyenDung);
        }

        // POST: ViTriTuyenDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhongBanID,TenViTriTuyenDung,Title,Request,Number,Id,LoaiId,TrangThaiId,IsDelete,NgayXoa")] ViTriTuyenDung viTriTuyenDung)
        {
            if (id != viTriTuyenDung.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viTriTuyenDung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViTriTuyenDungExists(viTriTuyenDung.Id))
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
            return View(viTriTuyenDung);
        }

        // GET: ViTriTuyenDungs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ViTriTuyenDungs == null)
            {
                return NotFound();
            }

            var viTriTuyenDung = await _context.ViTriTuyenDungs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viTriTuyenDung == null)
            {
                return NotFound();
            }

            return View(viTriTuyenDung);
        }

        // POST: ViTriTuyenDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ViTriTuyenDungs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ViTriTuyenDungs'  is null.");
            }
            var viTriTuyenDung = await _context.ViTriTuyenDungs.FindAsync(id);
            if (viTriTuyenDung != null)
            {
                _context.ViTriTuyenDungs.Remove(viTriTuyenDung);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViTriTuyenDungExists(int id)
        {
          return (_context.ViTriTuyenDungs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

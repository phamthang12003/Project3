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
    public class UngViensController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UngViensController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UngViens
        public async Task<IActionResult> Index()
        {
              return _context.UngViens != null ? 
                          View(await _context.UngViens.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.UngViens'  is null.");
        }

        // GET: UngViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UngViens == null)
            {
                return NotFound();
            }

            var ungVien = await _context.UngViens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ungVien == null)
            {
                return NotFound();
            }

            return View(ungVien);
        }

        // GET: UngViens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UngViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GioiTinh,Tuoi,TenUngVien,DiaChi,ViTriUngTuyen,KinhNghiemLamViec,Id,LoaiId,TrangThaiId,IsDelete,NgayXoa")] UngVien ungVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ungVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ungVien);
        }

        // GET: UngViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UngViens == null)
            {
                return NotFound();
            }

            var ungVien = await _context.UngViens.FindAsync(id);
            if (ungVien == null)
            {
                return NotFound();
            }
            return View(ungVien);
        }

        // POST: UngViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GioiTinh,Tuoi,TenUngVien,DiaChi,ViTriUngTuyen,KinhNghiemLamViec,Id,LoaiId,TrangThaiId,IsDelete,NgayXoa")] UngVien ungVien)
        {
            if (id != ungVien.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ungVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UngVienExists(ungVien.Id))
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
            return View(ungVien);
        }

        // GET: UngViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UngViens == null)
            {
                return NotFound();
            }

            var ungVien = await _context.UngViens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ungVien == null)
            {
                return NotFound();
            }

            return View(ungVien);
        }

        // POST: UngViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UngViens == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UngViens'  is null.");
            }
            var ungVien = await _context.UngViens.FindAsync(id);
            if (ungVien != null)
            {
                _context.UngViens.Remove(ungVien);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UngVienExists(int id)
        {
          return (_context.UngViens?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

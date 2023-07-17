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
    public class NhanVienPhuTrachTuyenDungsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NhanVienPhuTrachTuyenDungsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NhanVienPhuTrachTuyenDungs
        public async Task<IActionResult> Index()
        {
              return _context.NhanVienPhuTrachTuyenDungs != null ? 
                          View(await _context.NhanVienPhuTrachTuyenDungs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.NhanVienPhuTrachTuyenDungs'  is null.");
        }

        // GET: NhanVienPhuTrachTuyenDungs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NhanVienPhuTrachTuyenDungs == null)
            {
                return NotFound();
            }

            var nhanVienPhuTrachTuyenDung = await _context.NhanVienPhuTrachTuyenDungs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nhanVienPhuTrachTuyenDung == null)
            {
                return NotFound();
            }

            return View(nhanVienPhuTrachTuyenDung);
        }

        // GET: NhanVienPhuTrachTuyenDungs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NhanVienPhuTrachTuyenDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ViTriTuyenDungId,NhanVienId,Id,LoaiId,TrangThaiId,IsDelete,NgayXoa")] NhanVienPhuTrachTuyenDung nhanVienPhuTrachTuyenDung)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhanVienPhuTrachTuyenDung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhanVienPhuTrachTuyenDung);
        }

        // GET: NhanVienPhuTrachTuyenDungs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NhanVienPhuTrachTuyenDungs == null)
            {
                return NotFound();
            }

            var nhanVienPhuTrachTuyenDung = await _context.NhanVienPhuTrachTuyenDungs.FindAsync(id);
            if (nhanVienPhuTrachTuyenDung == null)
            {
                return NotFound();
            }
            return View(nhanVienPhuTrachTuyenDung);
        }

        // POST: NhanVienPhuTrachTuyenDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ViTriTuyenDungId,NhanVienId,Id,LoaiId,TrangThaiId,IsDelete,NgayXoa")] NhanVienPhuTrachTuyenDung nhanVienPhuTrachTuyenDung)
        {
            if (id != nhanVienPhuTrachTuyenDung.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhanVienPhuTrachTuyenDung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienPhuTrachTuyenDungExists(nhanVienPhuTrachTuyenDung.Id))
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
            return View(nhanVienPhuTrachTuyenDung);
        }

        // GET: NhanVienPhuTrachTuyenDungs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NhanVienPhuTrachTuyenDungs == null)
            {
                return NotFound();
            }

            var nhanVienPhuTrachTuyenDung = await _context.NhanVienPhuTrachTuyenDungs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nhanVienPhuTrachTuyenDung == null)
            {
                return NotFound();
            }

            return View(nhanVienPhuTrachTuyenDung);
        }

        // POST: NhanVienPhuTrachTuyenDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NhanVienPhuTrachTuyenDungs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NhanVienPhuTrachTuyenDungs'  is null.");
            }
            var nhanVienPhuTrachTuyenDung = await _context.NhanVienPhuTrachTuyenDungs.FindAsync(id);
            if (nhanVienPhuTrachTuyenDung != null)
            {
                _context.NhanVienPhuTrachTuyenDungs.Remove(nhanVienPhuTrachTuyenDung);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanVienPhuTrachTuyenDungExists(int id)
        {
          return (_context.NhanVienPhuTrachTuyenDungs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using backEnd.Models;

namespace backEnd.Controllers
{
    public class FormInfoesController : Controller
    {
        private readonly TeanantContext _context;

        public FormInfoesController(TeanantContext context)
        {
            _context = context;
        }

        // GET: FormInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.FormInfo.ToListAsync());
        }

        // GET: FormInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formInfo = await _context.FormInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formInfo == null)
            {
                return NotFound();
            }

            return View(formInfo);
        }

        // GET: FormInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FormInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNo")] FormInfo formInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formInfo);
        }

        // GET: FormInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formInfo = await _context.FormInfo.FindAsync(id);
            if (formInfo == null)
            {
                return NotFound();
            }
            return View(formInfo);
        }

        // POST: FormInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNo")] FormInfo formInfo)
        {
            if (id != formInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormInfoExists(formInfo.Id))
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
            return View(formInfo);
        }

        // GET: FormInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formInfo = await _context.FormInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formInfo == null)
            {
                return NotFound();
            }

            return View(formInfo);
        }

        // POST: FormInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formInfo = await _context.FormInfo.FindAsync(id);
            _context.FormInfo.Remove(formInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormInfoExists(int id)
        {
            return _context.FormInfo.Any(e => e.Id == id);
        }
    }
}

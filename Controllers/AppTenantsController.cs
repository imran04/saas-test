using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using backEnd.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace backEnd.Controllers
{
    public class AppTenantsController : Controller
    {
        private readonly BackEndContext _context;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public AppTenantsController(BackEndContext context)
        {
            _context = context;
        }

        // GET: AppTenants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tenants.ToListAsync());
        }

        // GET: AppTenants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appTenant = await _context.Tenants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appTenant == null)
            {
                return NotFound();
            }

            return View(appTenant);
        }

        // GET: AppTenants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppTenants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Hostname")] AppTenant appTenant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appTenant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appTenant);
        }

        // GET: AppTenants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appTenant = await _context.Tenants.FindAsync(id);
            if (appTenant == null)
            {
                return NotFound();
            }
            return View(appTenant);
        }

        // POST: AppTenants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Hostname")] AppTenant appTenant)
        {
            if (id != appTenant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appTenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppTenantExists(appTenant.Id))
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
            return View(appTenant);
        }

        // GET: AppTenants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appTenant = await _context.Tenants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appTenant == null)
            {
                return NotFound();
            }

            return View(appTenant);
        }

        // POST: AppTenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appTenant = await _context.Tenants.FindAsync(id);
            _context.Tenants.Remove(appTenant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppTenantExists(int id)
        {
            return _context.Tenants.Any(e => e.Id == id);
        }
    }
}

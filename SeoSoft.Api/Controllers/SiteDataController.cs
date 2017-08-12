//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using SeoSoft.Models;

//namespace SeoSoft.Controllers
//{
//    public class SiteDataController : Controller
//    {
//        private readonly ParserContext _context;

//        public SiteDataController(ParserContext context)
//        {
//            _context = context;    
//        }

//        // GET: SiteDatas
//        public async Task<IActionResult> Index()
//        {
//            return Ok();
//        }

//        // GET: SiteDatas/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var siteData = await _context.SiteData
//                .SingleOrDefaultAsync(m => m.Id == id);
//            if (siteData == null)
//            {
//                return NotFound();
//            }

//            return View(siteData);
//        }

 

//        // POST: SiteDatas/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
   
//        public async Task<IActionResult> Create(SiteData siteData)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(siteData);
//                await _context.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            return Ok();
//        }

//        // GET: SiteDatas/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var siteData = await _context.SiteData.SingleOrDefaultAsync(m => m.Id == id);
//            if (siteData == null)
//            {
//                return NotFound();
//            }
//            return View(siteData);
//        }

//        // POST: SiteDatas/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Xpath,Data")] SiteData siteData)
//        {
//            if (id != siteData.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(siteData);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!SiteDataExists(siteData.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction("Index");
//            }
//            return View(siteData);
//        }

//        // GET: SiteDatas/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var siteData = await _context.SiteData
//                .SingleOrDefaultAsync(m => m.Id == id);
//            if (siteData == null)
//            {
//                return NotFound();
//            }

//            return View(siteData);
//        }

//        // POST: SiteDatas/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var siteData = await _context.SiteData.SingleOrDefaultAsync(m => m.Id == id);
//            _context.SiteData.Remove(siteData);
//            await _context.SaveChangesAsync();
//            return RedirectToAction("Index");
//        }

//        private bool SiteDataExists(int id)
//        {
//            return _context.SiteData.Any(e => e.Id == id);
//        }
//    }
//}

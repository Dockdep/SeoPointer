using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeoSoft.Models;

namespace SeoSoft.Controllers
{
	[Route("api/[controller]")]
	public class ProjectsController : Controller
	{
		private readonly ParserContext _context;

		public ProjectsController(ParserContext context)
		{
			_context = context;
		}

		// GET: Projects
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var projects = _context.Project.OrderBy(p => p.Id);
			return Json(await projects.ToListAsync());
		}

		// GET: Projects/Details/5
		[HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {

            var project = await _context.Project
                .SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return Json(project);
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{id}")]
        public async Task<IActionResult> Create([FromBody] Project project)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(project);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }

            }

            return Json(project);
        }

		// POST: Projects/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPut("{id}")]
		public async Task<IActionResult> Edit(int id, [FromBody] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
     
            }
         
            return Json(project);
        }

		// POST: Projects/Delete/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return Json("success");
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}

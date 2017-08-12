using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeoSoft.Models;
using System.Net.Http;
using System.IO;

namespace SeoSoft.Controllers
{
	[Route("api/[controller]")]
	public class CompetitorsController : Controller
    {
        private readonly ParserContext _context;

        public CompetitorsController(ParserContext context)
        {
            _context = context;    
        }

		[HttpGet]
		public async Task<IActionResult> Get()
        {
            try
            {
                var competitors = _context.Competitor.OrderBy(p => p.Id);
                return Json(await competitors.ToListAsync());
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
       
        }


		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
        {
            var competitor = await _context.Competitor
                .SingleOrDefaultAsync(m => m.Id == id);
            if (competitor == null)
            {
                return NotFound();
            }

            return Json(competitor);
        }



   
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Competitor competitor)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(competitor);
                    await _context.SaveChangesAsync();
                    CompetitorField Price = new CompetitorField()
                    {

                        Name = "Price",
                        CompetitorId = competitor.Id,
                        Regexp ="",
                        XPath =""
                    };
                    _context.Add(Price);
                    CompetitorField EventPrice = new CompetitorField()
                    {

                        Name = "EventPrice",
                        CompetitorId = competitor.Id,
                        Regexp = "",
                        XPath = ""
                    };
                    _context.Add(EventPrice);
                    CompetitorField Title = new CompetitorField()
                    {

                        Name = "Title",
                        CompetitorId = competitor.Id,
                        Regexp = "",
                        XPath = ""
                    };
                    _context.Add(Title);
                    await _context.SaveChangesAsync();

                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }

            }
            else {
                var message = string.Join(" | ", ModelState.Values
                      .SelectMany(v => v.Errors)
                      .Select(e => e.ErrorMessage));
                return Json( message);
            }

            return Json(competitor);
        }

		[HttpPut("{id}")]
		public async Task<IActionResult> Edit(int id, [FromBody] Competitor competitor)
        {
            if (id != competitor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competitor);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }

            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                                .Where(y => y.Count > 0)
                                .ToList();
                return Json(errors);
            }

            return Json(competitor);
        }


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
        {
            var competitor = await _context.Competitor.SingleOrDefaultAsync(m => m.Id == id);
            _context.Competitor.Remove(competitor);
            await _context.SaveChangesAsync();
            return Json("success");
        }

        [HttpGet("parser/{id}")]
        public async Task<IActionResult> Parser(int id)
        {


            var competitor = await _context.Competitor
                .Include(c=>c.CompetitorField)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (competitor == null)
            {
                return NotFound();
            }

            return Json(competitor);
        }

        private bool CompetitorExists(int id)
        {
            return _context.Competitor.Any(e => e.Id == id);
        }
    }
}

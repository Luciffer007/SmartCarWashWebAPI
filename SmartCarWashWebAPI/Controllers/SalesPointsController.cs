using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCarWashWebAPI.Database;
using SmartCarWashWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCarWashWebAPI.Controllers
{
    public class SalesPointsController : BaseController
    {

        public SalesPointsController(ApiContext context) : base(context)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesPoint>>> Get()
        {
            return await db.SalesPoints.Include(sp => sp.ProvidedProducts).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalesPoint>> Get(int id)
        {
            SalesPoint salesPoint = await db.SalesPoints.Include(sp => sp.ProvidedProducts).FirstOrDefaultAsync(x => x.Id == id);
            if (salesPoint == null)
            {
                return NotFound();
            }

            return new ObjectResult(salesPoint);
        }

        [HttpPost]
        public async Task<ActionResult<SalesPoint>> Post(SalesPoint salesPoint)
        {
            if (salesPoint == null)
            {
                return BadRequest();
            }

            db.SalesPoints.Add(salesPoint);
            await db.SaveChangesAsync();
            return Ok(salesPoint);
        }

        [HttpPut]
        public async Task<ActionResult<SalesPoint>> Put(SalesPoint salesPoint)
        {
            if (salesPoint == null)
            {
                return BadRequest();
            }

            if (!db.SalesPoints.Any(x => x.Id == salesPoint.Id))
            {
                return NotFound();
            }

            db.Update(salesPoint);
            await db.SaveChangesAsync();
            return Ok(salesPoint);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SalesPoint>> Delete(int id)
        {
            SalesPoint salesPoint = db.SalesPoints.FirstOrDefault(x => x.Id == id);
            if (salesPoint == null)
            {
                return NotFound();
            }

            db.SalesPoints.Remove(salesPoint);
            await db.SaveChangesAsync();
            return Ok(salesPoint);
        }
    }
}

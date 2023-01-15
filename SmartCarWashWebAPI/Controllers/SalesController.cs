using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCarWashWebAPI.Database;
using SmartCarWashWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCarWashWebAPI.Controllers
{
    public class SalesController : BaseController
    {
        public SalesController(ApiContext context) : base(context)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> Get()
        {
            return await db.Sales.Include(s => s.SalesData).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> Get(int id)
        {
            Sale sale = await db.Sales.Include(s => s.SalesData).FirstOrDefaultAsync(x => x.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return new ObjectResult(sale);
        }

        [HttpPost]
        public async Task<ActionResult<Sale>> Post(Sale sale)
        {
            if (sale == null)
            {
                return BadRequest();
            }

            db.Sales.Add(sale);
            await db.SaveChangesAsync();
            return Ok(sale);
        }

        [HttpPut]
        public async Task<ActionResult<Sale>> Put(Sale sale)
        {
            if (sale == null)
            {
                return BadRequest();
            }

            if (!db.Sales.Any(x => x.Id == sale.Id))
            {
                return NotFound();
            }

            db.Update(sale);
            await db.SaveChangesAsync();
            return Ok(sale);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Sale>> Delete(int id)
        {
            Sale sale = db.Sales.FirstOrDefault(x => x.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            db.Sales.Remove(sale);
            await db.SaveChangesAsync();
            return Ok(sale);
        }
    }
}

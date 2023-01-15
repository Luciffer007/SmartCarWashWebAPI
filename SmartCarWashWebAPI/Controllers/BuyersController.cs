using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCarWashWebAPI.Database;
using SmartCarWashWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCarWashWebAPI.Controllers
{
    public class BuyersController : BaseController
    {
        public BuyersController(ApiContext context) : base(context)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buyer>>> Get()
        {
            return await db.Buyers.Include(b => b.Sales).ThenInclude(s => s.SalesData).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Buyer>> Get(int id)
        {
            Buyer buyer = await db.Buyers.Include(b => b.Sales).FirstOrDefaultAsync(x => x.Id == id);
            if (buyer == null)
            {
                return NotFound();
            }

            /*IEnumerable<Sale> sales = await db.Sales.ToListAsync();*/
            /*List<int> salesIds = db.Sales.Where(s => s.BuyerId == buyer.Id).Select(s => s.Id).ToList();
            buyer.SalesIds.AddRange(salesIds);*/
            return new ObjectResult(buyer);
        }

        [HttpPost]
        public async Task<ActionResult<Buyer>> Post(Buyer buyer)
        {
            if (buyer == null)
            {
                return BadRequest();
            }

            db.Buyers.Add(buyer);
            await db.SaveChangesAsync();
            return Ok(buyer);
        }

        [HttpPut]
        public async Task<ActionResult<Buyer>> Put(Buyer buyer)
        {
            if (buyer == null)
            {
                return BadRequest();
            }

            if (!db.Buyers.Any(x => x.Id == buyer.Id))
            {
                return NotFound();
            }

            db.Update(buyer);
            await db.SaveChangesAsync();
            return Ok(buyer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Buyer>> Delete(int id)
        {
            Buyer buyer = db.Buyers.FirstOrDefault(x => x.Id == id);
            if (buyer == null)
            {
                return NotFound();
            }

            db.Buyers.Remove(buyer);
            await db.SaveChangesAsync();
            return Ok(buyer);
        }
    }
}

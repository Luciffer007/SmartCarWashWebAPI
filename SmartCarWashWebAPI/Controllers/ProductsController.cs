using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCarWashWebAPI.Database;
using SmartCarWashWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCarWashWebAPI.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(ApiContext context) : base(context)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await db.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            Product product = await db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return new ObjectResult(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<Product>> Put(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (!db.Products.Any(x => x.Id == product.Id))
            {
                return NotFound();
            }

            db.Update(product);
            await db.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            Product product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return Ok(product);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SmartCarWashWebAPI.Database;

namespace SmartCarWashWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        public readonly ApiContext db;

        public BaseController(ApiContext context)
        {
            db = context;
        }
    }
}

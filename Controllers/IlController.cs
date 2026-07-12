using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("[controller]")] // 👈 ruta será /il
    public class IlController : ControllerBase
    {
        private readonly MyDbContext _context;

        public IlController(MyDbContext context)
        {
            _context = context;
        }

        // GET /il
        [HttpGet]
        public IActionResult GetIl()
        {
            var il = _context.Il.ToList();
            return Ok(il);
        }

        // GET /il/5
        [HttpGet("{id}")]
        public IActionResult GetIl(int id)
        {
            var il = _context.Il.Find(id);
            if (il == null) return NotFound();
            return Ok(il);
        }
    }
}

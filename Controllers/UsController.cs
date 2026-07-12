using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("[controller]")] // 👈 ruta será /us
    public class UsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UsController(MyDbContext context)
        {
            _context = context;
        }

        // GET /us
        [HttpGet]
        public IActionResult GetUs()
        {
            var us = _context.Us.ToList();
            return Ok(us);
        }

        // GET /us/5
        [HttpGet("{id}")]
        public IActionResult GetUs(int id)
        {
            var us = _context.Us.Find(id);
            if (us == null) return NotFound();
            return Ok(us);
        }
    }
}

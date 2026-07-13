using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("[controller]")] // 👈 ruta será /Coco
    public class CocoController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CocoController(MyDbContext context)
        {
            _context = context;
        }

        // GET /Coco
        [HttpGet]
        public IActionResult GetCoco()
        {
            var Coco = _context.Coco.ToList();
            return Ok(Coco);
        }

        // GET /Coco/5
        [HttpGet("{id}")]
        public IActionResult GetCoco(int id)
        {
            var Coco = _context.Coco.Find(id);
            if (Coco == null) return NotFound();
            return Ok(Coco);
        }
    }
}

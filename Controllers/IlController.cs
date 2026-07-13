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
        [HttpGet("{id:int}")]
        public IActionResult GetIlById(int id)
        {
            var il = _context.Il.Find(id);
            if (il == null) return NotFound();
            return Ok(il);
        }

        // GET /il/pr/exacto/COOTRASAR
        [HttpGet("pr/exacto/{pr}")]
        public IActionResult GetIlByPrExact(string pr)
        {
            var il = _context.Il
                .Where(i => i.pr == pr)
                .ToList();

            if (!il.Any()) return NotFound();
            return Ok(il);
        }

        // GET /il/pr/like/coop
        [HttpGet("pr/like/{texto}")]
        public IActionResult GetIlByPrLike(string texto)
        {
            var il = _context.Il
                .Where(i => i.pr.Contains(texto)) // 👈 LIKE '%texto%'
                .ToList();

            if (!il.Any()) return NotFound();
            return Ok(il);
        }

        // POST /il
        [HttpPost]
        public async Task<IActionResult> CreateIl([FromBody] Il nuevoIl)
        {
            _context.Il.Add(nuevoIl);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetIlByIdAsync), new { id = nuevoIl.Id }, nuevoIl);
        }

        // GET /il/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIlByIdAsync(int id)
        {
            var il = await _context.Il.FindAsync(id);
            if (il == null) return NotFound();
            return Ok(il);
        }

        // PUT /il/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIl(int id, [FromBody] Il ilActualizado)
        {
            var il = await _context.Il.FindAsync(id);
            if (il == null) return NotFound();

            // Actualiza los campos necesarios
            il.Nombre = ilActualizado.Nombre;
            il.Descripcion = ilActualizado.Descripcion;

            await _context.SaveChangesAsync();
            return Ok(il);
        }

    }
}

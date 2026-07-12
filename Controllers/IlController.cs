using Microsoft.AspNetCore.Mvc;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class UsController : ControllerBase
{
    private readonly MyDbContext _context;

    public UsController(MyDbContext context)
    {
        _context = context;
    }

    // GET api/us
    [HttpGet]
    public IActionResult GetUs()
    {
        var il = _context.Us.ToList();
        return Ok(il);
    }

    // GET api/us/5
    [HttpGet("{id}")]
    public IActionResult GetUs(int id)
    {
        var il = _context.Us.Find(id);
        if (il == null) return NotFound();
        return Ok(il);
    }
}

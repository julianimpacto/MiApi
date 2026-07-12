using Microsoft.AspNetCore.Mvc;
using System.Linq;

[ApiController]
[Route("[controller]")]

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
        var us = _context.Us.ToList();
        return Ok(us);
    }

    // GET api/us/5
    [HttpGet("{id}")]
    public IActionResult GetUs(int id)
    {
        var us = _context.Us.Find(id);
        if (us == null) return NotFound();
        return Ok(us);
    }
}

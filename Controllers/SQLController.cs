using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("sql")]
public class SqlController : ControllerBase
{
    private readonly MyDbContext _context;

    public SqlController(MyDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Ejecutar([FromBody] string query)
    {
        try
        {
            var result = await _context.Database.ExecuteSqlRawAsync(query);
            return Ok(new { FilasAfectadas = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}

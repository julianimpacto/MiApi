[ApiController]
[Route("sql")]
public class SqlDirectController : ControllerBase
{
    private readonly string _connectionString;

    public SqlDirectController(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    [HttpPost]
    public IActionResult Ejecutar([FromBody] SqlRequest request)
    {
        try
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            using var cmd = new MySqlCommand(request.Query, conn);
            int filas = cmd.ExecuteNonQuery();
            return Ok(new { FilasAfectadas = filas });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = ex.Message });
        }
    }
}

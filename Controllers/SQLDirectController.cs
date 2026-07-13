using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

[ApiController]
[Route("sql")]
public class SqlDirectController : ControllerBase
{
    private readonly string _connectionString;

    public SqlDirectController(IConfiguration config)
    {
        // Lee la cadena de conexión desde appsettings.json
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    [HttpPost]
    public IActionResult Ejecutar([FromBody] string query)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
        using var cmd = new MySqlCommand(query, conn);
        int filas = cmd.ExecuteNonQuery();
        return Ok(new { FilasAfectadas = filas });
    }
}

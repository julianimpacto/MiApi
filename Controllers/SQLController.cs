using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MiApi.Models;
using System.Globalization;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SqlController : ControllerBase
    {
        private readonly string _connectionString;

        public SqlController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        // CREATE
        [HttpPost]
        public IActionResult Create([FromBody] SqlRequest request) => EjecutarNonQuery(request.Query);

        // READ
        [HttpGet]
        public IActionResult Read([FromQuery] string query)
        {
            try
            {
                using var conn = new MySqlConnection(_connectionString);
                conn.Open();
                using var cmd = new MySqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                var resultados = new List<Dictionary<string, object>>();
                while (reader.Read())
                {
                    var fila = new Dictionary<string, object>();
                    ffor(int i = 0; i < reader.FieldCount; i++)
{
                        fila[reader.GetName(i)] = reader.IsDBNull(i)
                            ? null
                            : Convert.ToString(reader.GetValue(i), System.Globalization.CultureInfo.InvariantCulture);
                    }

                    resultados.Add(fila);
                }

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        // UPDATE
        [HttpPut]
        public IActionResult Update([FromBody] SqlRequest request
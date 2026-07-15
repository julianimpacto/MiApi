using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MiApi.Models;

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
        public IActionResult Create([FromBody] SqlRequest request)
        {
            return EjecutarNonQuery(request.Query);
        }

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
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        object valor = reader.GetValue(i);

                        // 🔹 Normalización segura para Swagger
                        if (valor == DBNull.Value)
                            fila[reader.GetName(i)] = null;
                        else if (valor is int || valor is long || valor is short)
                            fila[reader.GetName(i)] = Convert.ToInt32(valor);
                        else if (valor is decimal || valor is double || valor is float)
                            fila[reader.GetName(i)] = Convert.ToDouble(valor);
                        else if (valor is bool b)
                            fila[reader.GetName(i)] = b;
                        else
                            fila[reader.GetName(i)] = valor.ToString();
                    }
                    resultados.Add(fila);
                }

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        // UPDATE
        [HttpPut]
        public IActionResult Update([FromBody] SqlRequest request)
        {
            return EjecutarNonQuery(request.Query);
        }

        // DELETE
        [HttpDelete]
        public IActionResult Delete([FromBody] SqlRequest request)
        {
            return EjecutarNonQuery(request.Query);
        }

        // 🔹 Método auxiliar para INSERT/UPDATE/DELETE
        private IActionResult EjecutarNonQuery(string query)
        {
            try
            {
                using var conn = new MySqlConnection(_connectionString);
                conn.Open();
                using var cmd = new MySqlCommand(query, conn);
                int filas = cmd.ExecuteNonQuery();
                return Ok(new { FilasAfectadas = filas });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}

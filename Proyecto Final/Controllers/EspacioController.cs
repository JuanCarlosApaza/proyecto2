using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Modelo;

namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspacioController : ControllerBase
    {
        public readonly DbConexion dbConexion;
        public EspacioController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }

        [HttpGet]
        public async Task<ActionResult<List<Espacio>>> Get()
        {
            var espacio = await dbConexion.Espacio.ToListAsync();
            return Ok(espacio);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Espacio espacio)
        {
            if (espacio == null)
            {
                return BadRequest("El objeto esta vacio");
            }
            dbConexion.Espacio.Add(espacio);
            await dbConexion.SaveChangesAsync();
            return Ok("Se inserto correctamente");
        }
        [HttpPut("idespacio")]
        public async Task<ActionResult> Update(Espacio espacio, int idespacio)
        {
            if (espacio == null)
            {
                return BadRequest("Objeto Vacio");
            }
            if (idespacio == 0)
            {
                return BadRequest("El id de espacio esta vacio");
            }
            var existeespacio = await dbConexion.Espacio.FirstOrDefaultAsync(e => e.idespacio == idespacio);
            if (existeespacio == null)
            {
                return NotFound("El id no fue encontrado");
            }
            //modifico los campos
            existeespacio.nombre = espacio.nombre;
            existeespacio.ubicacion = espacio.ubicacion;
            await dbConexion.SaveChangesAsync();
            return Ok(existeespacio);

        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Modelo;

namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        public readonly DbConexion dbConexion;
        public PersonaController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }

        [HttpGet]
        public async Task<ActionResult<List<Persona>>> Get()
        {
            var persona = await dbConexion.Persona.ToListAsync();
            return Ok(persona);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Persona persona)
        {
            if (persona == null)
            {
                return BadRequest("El objeto esta vacio");
            }
            dbConexion.Persona.Add(persona);
            await dbConexion.SaveChangesAsync();
            return Ok($"Se inserto correctamente{persona.idpersona}");
        }

        [HttpPut("idpersona")]
        public async Task<ActionResult> Update(Persona persona, int idpersona)
        {
            if (persona == null)
            {
                return BadRequest("Objeto Vacio");
            }
            if (idpersona == 0)
            {
                return BadRequest("El id de organizador esta vacio");
            }
            var existepersona = await dbConexion.Persona.FirstOrDefaultAsync(p => p.idpersona == idpersona);
            if (existepersona == null)
            {
                return NotFound("El id no fue encontrado");
            }
            //modifico campos
            existepersona.nombre = persona.nombre;
            existepersona.apellido = persona.apellido;
            existepersona.telefono = persona.telefono;
            await dbConexion.SaveChangesAsync();
            return Ok(existepersona);

        }
    }
}

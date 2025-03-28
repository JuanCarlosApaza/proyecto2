using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Modelo;

namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        public readonly DbConexion dbConexion;
        public EventoController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get()
        {
            var evento = await dbConexion.Evento.Include(e => e.espacio).ToListAsync();
            return Ok(evento);
        }
        [HttpPost]
        public async Task<ActionResult<Evento>> Post([FromBody] Evento evento)
        {
            if (evento == null)
            {
                return BadRequest("Objeto es Vacio");
            }
            var Idespacio = await dbConexion.Espacio.FindAsync(evento.idespacio);//verificar si existe ese id: organizador existe  en tabla organizador
            if (Idespacio == null)
            {
                return BadRequest("El id espacio no existe");
            }
            //evento.idorganizador = Idorganizador.idorganizador;
            evento.espacio = null;
            dbConexion.Evento.Add(evento);
            await dbConexion.SaveChangesAsync();
            return Ok(evento);

        }
        [HttpPut("idevento")]
        public async Task<ActionResult> Update(Evento evento, int idevento)
        {
            if (evento == null)
            {
                return BadRequest("Objeto Vacio");
            }
            if (idevento == 0)
            {
                return BadRequest("El id de evento esta vacio");
            }
            var existeevento = await dbConexion.Evento.FirstOrDefaultAsync(e => e.idevento == idevento);
            if (existeevento == null)
            {
                return NotFound("El id no fue encontrado");
            }
            var espacioExiste = await dbConexion.Espacio.FindAsync(evento.idespacio);
            if (espacioExiste == null)
            {
                return BadRequest("El ID del espacio no existe.");
            }
            //modifico los campos
            existeevento.nombre = evento.nombre;
            existeevento.estado = evento.estado;
            existeevento.precio = evento.precio;
            existeevento.idespacio = evento.idespacio;
            await dbConexion.SaveChangesAsync();
            return Ok(existeevento);

        }

    }
}

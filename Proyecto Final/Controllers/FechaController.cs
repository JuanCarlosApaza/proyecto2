using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Modelo;
namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FechaController : ControllerBase
    {
        private readonly DbConexion dbConexion;

        public FechaController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }

        [HttpGet]
        public async Task<ActionResult<List<Fecha>>> GetAll()
        {
            var lista = await dbConexion.Fecha
                .Include(r => r.evento)
                .ToListAsync();

            return Ok(lista);
        }
        [HttpPost]
        public async Task<ActionResult<Fecha>> Post([FromBody] Fecha fecha)
        {
            if (fecha == null)
            {
                return BadRequest("no se pudo encontrar el objeto");
            }
            var IdFecha = await dbConexion.Evento.FindAsync(fecha.idevento);
            if (IdFecha == null)
            {
                return BadRequest("el id fecha no existe");
            }
            fecha.evento = null;
            dbConexion.Fecha.Add(fecha);
            await dbConexion.SaveChangesAsync();
            return Ok("Se inserto correctamente");
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(Fecha fecha, int idfecha)
        {
            if (fecha == null)
            {
                return BadRequest("objeto vacio");
            }
            if (idfecha == 0)
            {
                return BadRequest("el id de fecha esta vacio");
            }
            var existefecha = await dbConexion.Fecha.FirstOrDefaultAsync(p => p.id == idfecha);
            if (existefecha == null)
            {
                return NotFound("el id fecha no fue encontrado");
            }
            var idevento = await dbConexion.Evento.FindAsync(fecha.idevento);
            if (idevento == null)
            {
                return BadRequest("el id evento no existe");
            }

            fecha.evento = null;
            

            //modifica los campos del id
            existefecha.fecha_inicio = fecha.fecha_inicio;
            existefecha.fecha_final = fecha.fecha_final;
            existefecha.idevento =fecha.idevento;
            await dbConexion.SaveChangesAsync();
            return Ok($"se modifico los campos ");
        }

    }
}

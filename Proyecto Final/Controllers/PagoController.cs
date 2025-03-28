using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.Data;
using Proyecto_Final.Modelo;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        public readonly DbConexion dbConexion;
        public PagoController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }
        [HttpGet]
        public async Task<ActionResult<List<Pago>>> Get()
        {
            var pago = await dbConexion.Pago.ToListAsync();
            return Ok(pago);
        }
        [HttpPost]
        public async Task<ActionResult<Pago>> Post([FromBody] Pago pago)
        {
            if (pago == null)
            {
                return BadRequest("Objeto es Vacio");
            }
            dbConexion.Pago.Add(pago);
            await dbConexion.SaveChangesAsync();

            return Ok("El pago se inserto con exito");
        }
        [HttpPut("idPago")]
        public async Task<ActionResult> Update(Pago pago, int idpago)
        {
            if (pago == null)
            {
                return BadRequest("Objeto Vacio");
            }
            if (idpago == 0)
            {
                return BadRequest("El id de pago esta vacio");
            }
            var existepago = await dbConexion.Pago.FirstOrDefaultAsync(a => a.id == idpago);
            if (existepago == null)
            {
                return NotFound("El id no fue encontrado");
            }
            existepago.fecha = pago.fecha;
            existepago.estado = pago.estado;
            existepago.monto = pago.monto;
            await dbConexion.SaveChangesAsync();
            return Ok("Insertado correctamente");
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Modelo;

namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        public readonly DbConexion dbConexion;
        public TarjetaController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }
        [HttpGet]
        public async Task<ActionResult<List<Tarjeta>>> GetAll()
        {
            var tarjeta = await dbConexion.Tarjeta.Include(r => r.pago).ToListAsync();
            return Ok(tarjeta);
        }
        [HttpPost]
        public async Task<ActionResult<Tarjeta>> Post([FromBody] Tarjeta tarjeta)
        {
            if (tarjeta == null)
            {
                return BadRequest("Objeto es Vacio");
            }

            var Idpago = await dbConexion.Evento.FindAsync(tarjeta.idPago);
            if (Idpago == null)
            {
                return BadRequest("El id pago no existe");
            }
            tarjeta.pago = null;
            dbConexion.Tarjeta.Add(tarjeta);
            await dbConexion.SaveChangesAsync();

            return Ok("La tarjeta se inserto con exito");
        }
        [HttpPut("idTarjeta")]
        public async Task<ActionResult> Update(Tarjeta tarjeta, int idtarjeta)
        {
            if (tarjeta == null)
            {
                return BadRequest("Objeto Vacio");
            }
            if (idtarjeta == 0)
            {
                return BadRequest("El id de tarjeta esta vacio");
            }
            var existeTarjeta = await dbConexion.Tarjeta.FirstOrDefaultAsync(a => a.id == idtarjeta);
            if (existeTarjeta == null)
            {
                return NotFound("El id no fue encontrado");
            }
            var pagoExiste = await dbConexion.Evento.FindAsync(tarjeta.idPago);
            if (pagoExiste == null)
            {
                return BadRequest("El ID del pago no existe.");
            }
            existeTarjeta.nombreTitular = tarjeta.nombreTitular;
            existeTarjeta.fechaVencimiento = tarjeta.fechaVencimiento;
            existeTarjeta.cvv = tarjeta.cvv;
            existeTarjeta.idPago = tarjeta.idPago;
            await dbConexion.SaveChangesAsync();
            return Ok("Insertado correctamente");
        }
    }
}

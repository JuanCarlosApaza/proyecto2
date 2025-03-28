using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.Data;
using Proyecto_Final.Modelo;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrasnferenciaController : ControllerBase
    {
        public readonly DbConexion dbConexion;
        public TrasnferenciaController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }
        [HttpGet]
        public async Task<ActionResult<List<Transferencia>>> GetAll()
        {
            var transferencias = await dbConexion.Transferencia.Include(r => r.pago).ToListAsync();
            return Ok(transferencias);
        }
        [HttpPost]
        public async Task<ActionResult<Transferencia>> Post([FromBody] Transferencia transferencia)
        {
            if (transferencia == null)
            {
                return BadRequest("Objeto es Vacio");
            }

            var Idpago = await dbConexion.Evento.FindAsync(transferencia.idPago);
            if (Idpago == null)
            {
                return BadRequest("El id pago no existe");
            }
            transferencia.pago = null;
            dbConexion.Transferencia.Add(transferencia);
            await dbConexion.SaveChangesAsync();

            return Ok("La transferencia se inserto con exito");
        }
        [HttpPut("idTransferencia")]
        public async Task<ActionResult> Update(Transferencia transferencia, int idTraferencia)
        {
            if (transferencia == null)
            {
                return BadRequest("Objeto Vacio");
            }
            if (idTraferencia == 0)
            {
                return BadRequest("El id de transferencia esta vacio");
            }
            var existetranferencia = await dbConexion.Transferencia.FirstOrDefaultAsync(a => a.id == idTraferencia);
            if (existetranferencia == null)
            {
                return NotFound("El id no fue encontrado");
            }
            var pagoExiste = await dbConexion.Evento.FindAsync(transferencia.idPago);
            if (pagoExiste == null)
            {
                return BadRequest("El ID del pago no existe.");
            }
            existetranferencia.nombreBanco = transferencia.nombreBanco;
            existetranferencia.idPago = transferencia.idPago;
            await dbConexion.SaveChangesAsync();
            return Ok("Se inserto con exito");
        }
    }
}

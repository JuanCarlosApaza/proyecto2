using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.Data;
using Proyecto_Final.Modelo;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoletoController : ControllerBase
    {
        public readonly DbConexion dbConexion;
        public BoletoController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }
        [HttpGet]
        public async Task<ActionResult<List<Boleto>>> GetAll()
        {
            var boletos = await dbConexion.Boleto
                .Include(b => b.pago)        // Incluye la relación con Pago
                .Include(b => b.evento)      // Incluye la relación con Evento
                .Include(b => b.usuario)     // Incluye la relación con usuario
                .ToListAsync();

            return Ok(boletos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Boleto>>> GetAll(int id)
        {
            var boletos = await dbConexion.Boleto
                .Where(b => b.evento.idevento == id) 
                .Include(b => b.pago)
                .Include(b => b.evento)
                .Include(b => b.usuario)
                .ToListAsync();

            if (boletos == null || boletos.Count == 0)
            {
                return NotFound($"No se encontraron boletos para el evento con ID {id}");
            }

            return Ok(boletos);
        }

        [HttpPost]
        public async Task<ActionResult<Boleto>> Post([FromBody] Boleto boleto)
        {
            if (boleto == null)
            {
                return BadRequest("Objeto es Vacio");
            }

            var Idpago = await dbConexion.Pago.FindAsync(boleto.idPago);
            if (Idpago == null)
            {
                return BadRequest("El id pago no existe");
            }
            boleto.pago = null;
            var IdUsuario = await dbConexion.Usuario.FindAsync(boleto.idUsuario);
            if (IdUsuario == null)
            {
                return BadRequest("El id usuario no existe");
            }
            boleto.usuario = null;
            var IdEvento = await dbConexion.Evento.FindAsync(boleto.idEvento);
            if (IdEvento == null)
            {
                return BadRequest("El id evento no existe");
            }
            boleto.evento = null;

            dbConexion.Boleto.Add(boleto);
            await dbConexion.SaveChangesAsync();

            return Ok("El boleto se inserto con exito");
        }
        [HttpPut("idBoleto")]
        public async Task<ActionResult> Update(Boleto boleto, int idboleto)
        {
            if (boleto == null)
            {
                return BadRequest("Objeto Vacio");
            }
            if (idboleto == 0)
            {
                return BadRequest("El id de boleto esta vacio");
            }
            var existeBoleto = await dbConexion.Boleto.FirstOrDefaultAsync(a => a.id == idboleto);
            if (existeBoleto == null)
            {
                return NotFound("El id no fue encontrado");
            }
            var pagoExiste = await dbConexion.Pago.FindAsync(boleto.idPago);
            if (pagoExiste == null)
            {
                return BadRequest("El ID del pago no existe.");
            }
            var usuarioExiste = await dbConexion.Usuario.FindAsync(boleto.idUsuario);
            if (usuarioExiste == null)
            {
                return BadRequest("El ID del usuario no existe.");
            }
            var eventoExiste = await dbConexion.Evento.FindAsync(boleto.idEvento);
            if (eventoExiste == null)
            {
                return BadRequest("El ID del evento no existe.");
            }
            existeBoleto.estado = boleto.estado;
            existeBoleto.idUsuario = boleto.idUsuario;
            existeBoleto.idEvento = boleto.idEvento;
            existeBoleto.idPago = boleto.idPago;
            await dbConexion.SaveChangesAsync();
            return Ok("Se modifico corectamente");
        }
    }
}

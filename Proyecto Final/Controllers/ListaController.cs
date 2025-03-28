using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Modelo;

namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaController : ControllerBase
    {
        private readonly DbConexion dbConexion;

        public ListaController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }

        [HttpGet]
        public async Task<ActionResult<List<Lista>>> GetAll()
        {
            var lista = await dbConexion.Lista
                .Include(r => r.boleto)
                .ToListAsync();

            return Ok(lista);
        }
        [HttpPost]
        public async Task<ActionResult<Lista>> Post([FromBody] Lista lista)
        {
            if (lista == null)
            {
                return BadRequest("no se pudo encontrar el objeto");
            }
            var IdLista = await dbConexion.Boleto.FindAsync(lista.idboleto);
            if (IdLista == null)
            {
                return BadRequest("el id boleto no existe");
            }
            lista.boleto = null;
            dbConexion.Lista.Add(lista);
            await dbConexion.SaveChangesAsync();
            return Ok("Se inserto correctamente");
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(Lista lista, int idlista)
        {
            if (lista == null)
            {
                return BadRequest("objeto vacio");
            }
            if (idlista == 0)
            {
                return BadRequest("el id de evento esta vacio");
            }
            var existelista = await dbConexion.Lista.FirstOrDefaultAsync(p => p.id == idlista);
            if (existelista == null)
            {
                return NotFound("el id evento no fue encontrado");
            }
            var idboleto = await dbConexion.Boleto.FindAsync(lista.idboleto);
            if (idboleto == null)
            {
                return BadRequest("el id boleto no existe");
            }
            var idusuario = await dbConexion.Usuario.FindAsync(lista.idusuario);
            if (idusuario == null)
            {
                return BadRequest("el id usuario no existe");
            }
            lista.boleto = null;
            lista.usuario = null;

            //modifica los campos del id
            existelista.idusuario = lista.idusuario;
            existelista.idboleto = lista.idboleto;
            existelista.estado = lista.estado;
            await dbConexion.SaveChangesAsync();
            return Ok($"se modifico los campos ");
        }
    }
}

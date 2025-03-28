using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Modelo;

namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspacioUsuarioController : ControllerBase
    {
        public readonly DbConexion dbConexion;
        public EspacioUsuarioController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }
        [HttpGet]
        public async Task<ActionResult<List<EspacioUsuario>>> GetAll()
        {
            var espaciosUsuarios = await dbConexion.EspacioUsuario.Include(eu => eu.usuario).ThenInclude(eu => eu.persona).Include(eu => eu.espacio).ToListAsync();
            return Ok(espaciosUsuarios);
        }
        [HttpPost]
        public async Task<ActionResult<EspacioUsuario>> Post([FromBody] EspacioUsuario espacioUsuario)
        {
            if (espacioUsuario == null)
            {
                return BadRequest("El objeto esta vacio");
            }
            var usuarioExistente = await dbConexion.Usuario.FindAsync(espacioUsuario.idusuario);
            if (usuarioExistente == null)
            {
                return BadRequest("El ID del usuario no existe");
            }
            var espacioExistente = await dbConexion.Espacio.FindAsync(espacioUsuario.idespacio);
            if (espacioExistente == null)
            {
                return BadRequest("El ID del espacio no existe");
            }
            espacioUsuario.usuario = null;
            espacioUsuario.espacio = null;

            dbConexion.EspacioUsuario.Add(espacioUsuario);
            await dbConexion.SaveChangesAsync();

            return Ok(espacioUsuario);
        }
        [HttpPut("idespaciousuario")]
        public async Task<ActionResult> Update(EspacioUsuario espacioUsuario, int idespaciousuario)
        {
            if (espacioUsuario == null)
            {
                return BadRequest("El objeto esta vacio");
            }
            if (idespaciousuario == 0)
            {
                return BadRequest("El ID del espacio usuario esta vacio");
            }
            var existeEspacioUsuario = await dbConexion.EspacioUsuario.FirstOrDefaultAsync(eu => eu.idespaciousuario == idespaciousuario);
            if (existeEspacioUsuario == null)
            {
                return NotFound("El ID no fue encontrado");
            }
            var usuarioExiste = await dbConexion.Usuario.FindAsync(espacioUsuario.idusuario);
            if (usuarioExiste == null)
            {
                return BadRequest("El ID del usuario no existe.");
            }
            var espacioExiste = await dbConexion.Espacio.FindAsync(espacioUsuario.idespacio);
            if (espacioExiste == null)
            {
                return BadRequest("El ID del espacio no existe.");
            }
            existeEspacioUsuario.idusuario = espacioUsuario.idusuario;
            existeEspacioUsuario.idespacio = espacioUsuario.idespacio;
            await dbConexion.SaveChangesAsync();
            var espacioUsuarioActualizado = await dbConexion.EspacioUsuario.Include(eu => eu.usuario).ThenInclude(u => u.persona).Include(eu => eu.espacio).FirstOrDefaultAsync(eu => eu.idespaciousuario == idespaciousuario);
            return Ok(espacioUsuarioActualizado);

        }



    }
}

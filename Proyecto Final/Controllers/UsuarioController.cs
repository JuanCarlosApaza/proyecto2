using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Modelo;

namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly DbConexion dbConexion;
        public UsuarioController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            var usuario = await dbConexion.Usuario.Include(p => p.persona).ToListAsync();
            return Ok(usuario);
        }
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post([FromForm] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Objeto es Vacio");
            }
            var Idpersona = await dbConexion.Persona.FindAsync(usuario.idpersona);
            if (Idpersona == null)
            {
                return BadRequest("El id persona no existe");
            }
            
            usuario.persona = null;
            dbConexion.Usuario.Add(usuario);
            await dbConexion.SaveChangesAsync();
            return Ok("usuario creado con exito");

        }

        [HttpPut("idusuario")]
        public async Task<ActionResult> Update(Usuario usuario, int idusuario)
        {
            if (usuario == null)
            {
                return BadRequest("Objeto Vacio");
            }
            if (idusuario == 0)
            {
                return BadRequest("El id de usuario esta vacio");
            }
            var existeusuario = await dbConexion.Usuario.FirstOrDefaultAsync(u => u.idusuario == idusuario);
            if (existeusuario == null)
            {
                return NotFound("El id no fue encontrado");
            }
            var personaExiste = await dbConexion.Persona.FindAsync(usuario.idpersona);
            if (personaExiste == null)
            {
                return BadRequest("El ID del persona no existe.");
            }
            //modifico los campos
            existeusuario.correo = usuario.correo;
            existeusuario.contrasena = usuario.contrasena;
            existeusuario.fecha_creacion = usuario.fecha_creacion;
            existeusuario.idpersona = usuario.idpersona;
            await dbConexion.SaveChangesAsync();
            return Ok(existeusuario);

        }
    }
}

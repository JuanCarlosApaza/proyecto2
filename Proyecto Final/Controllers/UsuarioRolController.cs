using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Modelo;

namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRolController : ControllerBase
    {
        public readonly DbConexion dbConexion;
        public UsuarioRolController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }
        [HttpGet]
        public async Task<ActionResult<List<UsuarioRol>>> GetAll()
        {
            var usuariorol = await dbConexion.UsuarioRol.Include(r => r.roles).Include(m => m.usuario).ToListAsync();
            return Ok(usuariorol);

        }
        [HttpPost]
        public async Task<ActionResult<RolModulos>> Post([FromBody] UsuarioRol usuariorol)
        {
            if (usuariorol == null)
            {
                return BadRequest("el objeto no existe");
            }
            var existeidrol = await dbConexion.Roles.FindAsync(usuariorol.idrol);
            var existeidusuario = await dbConexion.Usuario.FindAsync(usuariorol.idusuario);
            if (existeidusuario == null)
            {
                return NotFound("idusuario no encontrado");
            }
            if (existeidrol == null)
            {
                return NotFound("idrol no encontrado");
            }
            usuariorol.roles = null;
            usuariorol.usuario = null;
            dbConexion.UsuarioRol.Add(usuariorol);
            dbConexion.SaveChanges();
            return Ok("se instalo correctamente");
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(UsuarioRol usuariorol, int idusuariorol)
        {
            if (usuariorol == null)
            {
                return BadRequest("el objeto no fue encontrado");
            }
            if (idusuariorol == 0)
            {
                return BadRequest("el id esta vacio");
            }
            var existeidusuariorol = await dbConexion.UsuarioRol.FirstOrDefaultAsync(rm => rm.id == idusuariorol);
            if (existeidusuariorol == null)
            {
                return NotFound("el id de usuario rol no fue encontrado");
            }
            var idroles = await dbConexion.Roles.FindAsync(usuariorol.idrol);
            var idusuario = await dbConexion.Usuario.FindAsync(usuariorol.idusuario);
            if (idroles == null)
            {
                return NotFound("el id roles no se encontro");
            }
            if (idusuario == null)
            {
                return NotFound("el id usuario no se encontro");
            }
            existeidusuariorol.usuario = null;
            existeidusuariorol.roles = null;
            existeidusuariorol.idrol = usuariorol.idrol;
            existeidusuariorol.idusuario = usuariorol.idusuario;
            await dbConexion.SaveChangesAsync();
            return Ok("se guardo los datos correctamente");

        }


    }
}

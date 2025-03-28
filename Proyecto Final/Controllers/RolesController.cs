using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Modelo;

namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DbConexion dbConexion;
        public RolesController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }
        [HttpGet]
        public async Task<ActionResult<List<Roles>>> Get()
        { 
        var roles = await dbConexion.Roles.ToListAsync();
            return Ok(roles);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Roles roles)
        {
            if (roles == null)
            {
                return BadRequest("el obejto esta vacio");
            }
            dbConexion.Roles.Add(roles);
            await dbConexion.SaveChangesAsync();
            return Ok("rol guardado con exito");
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(Roles roles, int idrol) {
            if (roles == null)
            {
                return BadRequest("el objeto esta vacio");
            }
            if (idrol == 0)
            {
                return BadRequest("el idrol esta vacio");
                
            }
            var existerol = await dbConexion.Roles.FirstOrDefaultAsync(p=>p.id == idrol);
            if (existerol ==null)
            {
                return NotFound("el id no fue encontrado");
                
            }
            existerol.nombre_rol = roles.nombre_rol;
            existerol.fecha_creacion = roles.fecha_creacion;
            existerol.estado = roles.estado;
            await dbConexion.SaveChangesAsync();
            return Ok("rol actualizado con exito");
        }
    }
}

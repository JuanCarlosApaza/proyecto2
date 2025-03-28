using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.Modelo;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;

namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolModulosController : ControllerBase
    {
        public readonly DbConexion dbConexion;
        public RolModulosController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }
        [HttpGet]
        public async Task<ActionResult<List<RolModulos>>> GetAll() {
            var rolmodulos = await dbConexion.RolModulos.Include(r => r.roles).Include(m => m.modulos).ToListAsync();
            return Ok(rolmodulos);

        }
        [HttpPost]
        public async Task<ActionResult<RolModulos>> Post([FromBody] RolModulos rolmodulos)
        {
            if (rolmodulos == null)
            {
                return BadRequest("el objeto no existe");
            }
            var existeidrol = await dbConexion.Roles.FindAsync(rolmodulos.idrol);
            var existeidmodulo = await dbConexion.Modulos.FindAsync(rolmodulos.idmodulo);
            if (existeidmodulo == null)
            {
                return NotFound("idmodulo no encontrado");
            }
            if (existeidrol == null)
            {
                return NotFound("idrol no encontrado");
            }
            rolmodulos.roles = null;
            rolmodulos.modulos = null;
            dbConexion.RolModulos.Add(rolmodulos);
            dbConexion.SaveChanges();
            return Ok("se instalo correctamente");
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(RolModulos rolmodulos, int idrolmodulos)
        {
            if (rolmodulos == null )
            {
                return BadRequest("el objeto no fue encontrado");
            }
            if (idrolmodulos == 0)
            {
                return BadRequest("el id esta vacio");
            }
            var existerolmodulo = await dbConexion.RolModulos.FirstOrDefaultAsync(rm => rm.id == idrolmodulos);
            if (existerolmodulo == null)
            {
                return BadRequest("el id de rol modulo no fue encontrado");
            }
            var idroles = await dbConexion.Roles.FindAsync(rolmodulos.idrol);
            var idmodulos = await dbConexion.Modulos.FindAsync(rolmodulos.idmodulo);
            if (idroles == null)
            {
                return NotFound("el id roles no se encontro");
            }
            if (idmodulos == null)
            {
                return NotFound("el id modulos no se encontro");
            }
            existerolmodulo.modulos = null;
            existerolmodulo.roles = null;
            existerolmodulo.idrol = rolmodulos.idrol;
            existerolmodulo.idmodulo = rolmodulos.idmodulo;
            await dbConexion.SaveChangesAsync();
            return Ok("se guardo los datos correctamente");

        }


    }
}

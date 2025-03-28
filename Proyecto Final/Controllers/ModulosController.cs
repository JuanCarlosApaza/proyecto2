using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Modelo;

namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulosController : ControllerBase
    {
        public readonly DbConexion dbConexion;
        public ModulosController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }
        [HttpGet]
        public async Task<ActionResult<List<Modulos>>> Get() { 
        var modulos = await dbConexion.Modulos.ToListAsync();
            return Ok(modulos);

        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Modulos modulos)
        {
            if (modulos == null)
            {
                return BadRequest("el objeto esat vacio");
            }

            dbConexion.Modulos.Add(modulos);
            await dbConexion.SaveChangesAsync();
            return Ok("guardado con exito");
            
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(Modulos modulo,int idmodulo) {
            if (modulo == null){ return BadRequest("el obejto esta vacio");}
            if (idmodulo == 0) { return BadRequest("el id esta vacio"); }
            var existemodulo = await dbConexion.Modulos.FirstOrDefaultAsync(m => m.id == idmodulo);
            if (existemodulo == null)
            { 
            return NotFound("no se encontro el id en modulos");
            }
            existemodulo.nombre = modulo.nombre;
            existemodulo.ruta = modulo.ruta;
            await dbConexion.SaveChangesAsync();
            return Ok("cambios guardados");


        
        }
    }
}

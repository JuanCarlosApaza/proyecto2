using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Modelo;

namespace Proyecto_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnuncioController : ControllerBase
    {
        public readonly DbConexion dbConexion;
        private readonly string rutaBase = "wwwroot/Uploads/Anuncios/";
        public AnuncioController(DbConexion dbConexion)
        {
            this.dbConexion = dbConexion;
        }

        [HttpGet]
        public async Task<ActionResult<List<Anuncio>>> Get()
        {
            var anuncio = await dbConexion.Anuncio.Include(r => r.evento).ToListAsync();//recuperar del evento 
            return Ok(anuncio);
        }
        
        [HttpPost("GuardarImagen")]
        public async Task<string> GuardarImagen([FromForm] Imagen fichero)
        {
            var ruta = string.Empty;
            if (fichero.Archivo.Length > 0)
            {
                var nombreArchivo = Guid.NewGuid().ToString() + ".jpg";//resacatamo el nombre lo convierte a obejto de tipo cadeana
                ruta = $"wwwroot/Uploads/Anuncios/{nombreArchivo}";
                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    await fichero.Archivo.CopyToAsync(stream);
                }
            }
            ruta = ruta.Replace("wwwroot/", "");
            return ruta;
        }
        [HttpPost]
        public async Task<ActionResult<Anuncio>> Post([FromForm] Anuncio anuncio, [FromForm] Imagen fichero)
        {
            if (anuncio == null)
            {
                return BadRequest("El objeto Anuncio está vacío.");
            }

            var eventoExistente = await dbConexion.Evento.FindAsync(anuncio.idevento);
            if (eventoExistente == null)
            {
                return BadRequest("El ID del evento no existe.");
            }

            if (fichero?.Archivo == null || fichero.Archivo.Length == 0)
            {
                return BadRequest("Debe seleccionar una imagen.");
            }

            // Debe existir si i si la carpeta 
            if (!Directory.Exists(rutaBase))
            {
                Directory.CreateDirectory(rutaBase);
            }

            
            var ruta = string.Empty;
            if (fichero.Archivo.Length > 0)
            {
                var nombreArchivo = Guid.NewGuid().ToString() + ".jpg";
                ruta = $"wwwroot/Uploads/Anuncios/{nombreArchivo}";

                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    await fichero.Archivo.CopyToAsync(stream);
                }
            }

            ruta = ruta.Replace("wwwroot/", "");

            
            anuncio.imagen = ruta;
            anuncio.evento = null;

            dbConexion.Anuncio.Add(anuncio);
            await dbConexion.SaveChangesAsync();

            return Ok(anuncio);
        }

        [HttpPut("idanuncio")]
        public async Task<ActionResult> Update(Anuncio anuncio, int idanuncio)
        {
            if (anuncio == null)
            {
                return BadRequest("Objeto Vacio");
            }
            if (idanuncio == 0)
            {
                return BadRequest("El id de anuncio esta vacio");
            }
            var existeanuncio = await dbConexion.Anuncio.FirstOrDefaultAsync(a => a.idanuncio == idanuncio);
            if (existeanuncio == null)
            {
                return NotFound("El id no fue encontrado");
            }
            var eventoExiste = await dbConexion.Evento.FindAsync(anuncio.idevento);
            if (eventoExiste == null)
            {
                return BadRequest("El ID del evento no existe.");
            }
            //modifico los campos
            existeanuncio.titulo = anuncio.titulo;
            existeanuncio.imagen = anuncio.imagen;
            existeanuncio.descripcion = anuncio.descripcion;
            existeanuncio.idevento = anuncio.idevento;
            await dbConexion.SaveChangesAsync();
            return Ok(existeanuncio);

        }
    }
}

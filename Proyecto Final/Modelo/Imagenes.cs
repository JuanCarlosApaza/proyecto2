using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Modelo
{
    public class Imagenes
    {
        [Key]
        public int idimagen { get; set; }
        public string imagen { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Modelo
{
    public class Espacio
    {
        [Key]
        public int idespacio {  get; set; }

        [StringLength(50, ErrorMessage = "El minimo de caracteres es 10", MinimumLength = 10)]
        [Required(ErrorMessage ="Nombre es obligatorio")]
        public string nombre { get; set; }

        [StringLength(80, ErrorMessage = "El minimo de caracteres es 10", MinimumLength = 10)]
        [Required(ErrorMessage = "Ubicacion es obligatorio")]
        public string ubicacion { get; set; }

    }
}

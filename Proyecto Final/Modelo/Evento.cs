using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Modelo
{
    public class Evento
    {
        [Key]
        public int idevento { get; set; }
        [Required(ErrorMessage = "Nombre es requerido")]
        [StringLength(40, ErrorMessage = "No cumple con el minimo", MinimumLength = 10)]
        public string nombre { get; set; }


        [Required(ErrorMessage = "El estado es requerido")]
        [StringLength(10, ErrorMessage = "No cumple con el minimo de 5 caracteres", MinimumLength = 5)]
        public string estado { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(200, 1000)]
        public int precio { get; set; }

        public int idespacio { get; set; }
        [ForeignKey(nameof(idespacio))]
        public Espacio? espacio { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Modelo
{
    public class Anuncio
    {
        [Key]
        public int idanuncio { get; set; }

        [Required(ErrorMessage ="El titulo es obligatorio")]
        [StringLength(30,ErrorMessage ="El minimo de caracteres es 10",MinimumLength =10)]
        public string titulo { get; set; }

        [Required(ErrorMessage = "La imagen es obligatoria")]
        [StringLength(60, ErrorMessage = "El minimo de caracteres es 10", MinimumLength = 10)]
        public string imagen { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatoria")]
        [StringLength(200, ErrorMessage = "El minimo de caracteres es 10", MinimumLength = 10)]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        [StringLength(10, ErrorMessage = "No cumple con el minimo de 5 caracteres", MinimumLength = 5)]
        public string estado { get; set; }
        public int idevento { get; set; }
        [ForeignKey(nameof(idevento))]
        public Evento evento { get; set; }
        public int idusuario { get; set; }
        [ForeignKey(nameof(idusuario))]
        public Usuario usuario { get; set; }
    }
}

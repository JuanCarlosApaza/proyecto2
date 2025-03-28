using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Modelo
{
    public class Persona
    {
        [Key]
        public int idpersona { get; set; }
        [Required(ErrorMessage ="El Nombre es obligatorio")]
        [StringLength(80,ErrorMessage ="Minimo de caracteres es 4",MinimumLength =4)]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El Apellido es obligatorio")]
        [StringLength(80, ErrorMessage = "Minimo de caracteres es 4", MinimumLength = 4)]
        public string apellido { get; set; }

        [Required(ErrorMessage = "El Telefono es obligatorio")]
        public int telefono { get; set; }
    }
}

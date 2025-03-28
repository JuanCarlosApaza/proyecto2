using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Modelo
{
    public class Usuario
    {
        [Key]
        public int idusuario { get; set; }
        [Required(ErrorMessage = "El correo es obligatorio")]
        [StringLength(50,ErrorMessage ="Minimo de caracteres es 9",MinimumLength =9)]
        public string correo { get; set; }

        [Required(ErrorMessage = "La contrseña es obligatoria")]
        [StringLength(40, ErrorMessage = "Minimo de caracteres es 8", MinimumLength = 8)]
        public string contrasena { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime fecha_creacion {  get; set; }
        public string estado { get; set; }

        public int idpersona { get; set; }
        [ForeignKey(nameof(idpersona))]
        public Persona persona { get; set; }
        
    }
}

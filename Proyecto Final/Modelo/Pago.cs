using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Modelo
{
    public class Pago
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Campo fecha requerido")]
        [DataType(DataType.Date)]
        public DateTime fecha { get; set; }
        [Required(ErrorMessage = "Campo estado requerido")]
        [StringLength(20, ErrorMessage = "Maximo 20 caracteres y minimo 1", MinimumLength = 1)]
        public string estado { get; set; }= "Activo";
        [Required(ErrorMessage = "Campo monto requerido")]
        public int monto { get; set; }

    }
}

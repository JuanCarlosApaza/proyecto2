using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Modelo
{
    public class Roles
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage ="el nombre del rol es requerido")]
        [StringLength(50,ErrorMessage ="el nombre de debe estar entre 5 y 50",MinimumLength =5)]
        public string? nombre_rol { get; set; }

        public string estado { get; set; } = "Activo";
        [DataType (DataType.Date)]
        public DateTime fecha_creacion { get; set; } = DateTime.Now;

    }
}

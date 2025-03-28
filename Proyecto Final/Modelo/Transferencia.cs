using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Modelo
{
    public class Transferencia
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Campo nombre del banco es requerido")]
        [StringLength(100,ErrorMessage ="El nombre no cumple con los maximos ni minimos")]
        public string nombreBanco { get; set; }
        [Required(ErrorMessage = "Campo id pago es requerido")]
        public int idPago { get; set; }
        [ForeignKey(nameof(idPago))]
        public Pago pago { get; set; }
    }
}

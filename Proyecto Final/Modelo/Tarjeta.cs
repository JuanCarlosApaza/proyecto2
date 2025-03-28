using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Modelo
{
    public class Tarjeta
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage ="El nro de tarjeta requerido")]
        public int numeroTarjeta { get; set; }
        [Required(ErrorMessage = "El nombre del titular requerido")]
        [StringLength(100, ErrorMessage = "El nombre no cumple con los maximos ni minimos",MinimumLength =5)]
        public string nombreTitular { get; set; }
        [Required(ErrorMessage = "La fecha vencimiento de la tarjeta requerido")]
        [DataType(DataType.Date)]
        public DateTime fechaVencimiento { get; set; }
        [Required(ErrorMessage = "El cvv de la tarjeta requerido")]
        public int cvv { get; set; }
        [Required(ErrorMessage = "El id del pago requerido")]
        public int idPago { get; set; }
        [ForeignKey(nameof(idPago))]
        public Pago pago { get; set; }
    }
}

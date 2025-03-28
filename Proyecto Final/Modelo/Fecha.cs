using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Modelo
{
    public class Fecha
    {
        [Key]
        public int id { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_final { get; set; }

        public int idevento { get; set; }
        [ForeignKey (nameof(idevento))]
        public Evento? evento { get; set; }

    }
}

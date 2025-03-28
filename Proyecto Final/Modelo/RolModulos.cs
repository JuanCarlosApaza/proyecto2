using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Modelo
{
    public class RolModulos
    {
        [Key]
        public int id { get; set; }
        public int idrol { get; set; }
        [ForeignKey(nameof(idrol))]
        public Roles? roles { get; set; }
        public int idmodulo { get; set; }
        [ForeignKey(nameof(idmodulo))]
        public Modulos? modulos { get; set; }
    }
}

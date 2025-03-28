using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Modelo
{
    public class UsuarioRol
    {
        [Key]
        public int id { get; set; }

        public int idusuario { get; set; }
        [ForeignKey(nameof(idusuario))]
        public Usuario? usuario { get; set; }
        public int idrol { get; set; }
        [ForeignKey(nameof(idrol))]
        public Roles? roles { get; set; }

    }
}

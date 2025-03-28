using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Modelo
{
    public class EspacioUsuario
    {
        [Key]
        public int idespaciousuario { get; set; }
        public int idusuario { get; set; }
        [ForeignKey(nameof(idusuario))]
        public Usuario usuario { get; set; }
        public int idespacio { get; set; }
        [ForeignKey(nameof(idespacio))]
        public Espacio espacio { get; set; }
    }
}

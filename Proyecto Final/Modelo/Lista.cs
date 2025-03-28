using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Modelo
{
    public class Lista
    {
        [Key]
        public int id { get; set; }
        public bool estado { get; set; } = false;
        public int idusuario { get; set; }

        [ForeignKey (nameof(idusuario))]
        public Usuario? usuario { get; set; }
        public int idboleto { get; set; }
        [ForeignKey (nameof(idboleto))]
        public Boleto? boleto { get; set; }
        

    }
}

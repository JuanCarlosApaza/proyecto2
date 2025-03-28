using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Modelo
{
    public class Modulos
    {
        [Key]
        public int id { get; set; }
        [Required (ErrorMessage ="el campo nombre es necesario")]
        [StringLength (50,ErrorMessage = "el nombre del modulo debe estar entre", MinimumLength = 5 ) ]
         public string nombre { get; set; }
        [Required (ErrorMessage ="el campo ruta es necesario") ]
        [StringLength (30,ErrorMessage ="el campo ebe cumplir entre 30 y 5 ",MinimumLength =5)]
        public string ruta { get; set; }

    }
}

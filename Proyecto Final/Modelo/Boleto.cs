﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Modelo
{
    public class Boleto
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage ="Campo estado requerido")]
        [StringLength(20,ErrorMessage = "Maximo 20 caracteres y minimo 1",MinimumLength =1)]
        public string estado { get; set; } 
        public int idEvento { get; set; }
        public int idUsuario { get; set; }
        public int idPago { get; set; }
        [ForeignKey(nameof(idEvento))]
        public Evento? evento { get; set; }
        [ForeignKey(nameof(idUsuario))]
        public Usuario? usuario { get; set; }
        [ForeignKey(nameof(idPago))]
        public Pago? pago { get; set; }

    }
}

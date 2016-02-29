namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

   [Table("Agendamento")]
    public class Agendamento
    {
        [Key]
        public long agendamentoId { get; set; }

        public DateTime? dt_Agendamento { get; set; }

        public DateTime? dt_Marcacao { get; set; }

        public long? pessoaId { get; set; }

        public long? usuarioId { get; set; }

       

        public virtual Pessoa Pessoa { get; set; }

        public virtual User User { get; set; }
    }
}

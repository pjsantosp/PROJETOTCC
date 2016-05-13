namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Agendamento")]
    public class Agendamento
    {
        [Key]
        public long agendamentoId { get; set; }
        public long? pessoaId { get; set; }
        public long? usuarioId { get; set; }
        public long? clinicaId { get; set; }

        [Display(Name ="Observações")]
        [DataType(DataType.MultilineText)]
        public string observacoes { get; set; }

        [Display(Name ="Data do Agendamento")]
        public DateTime? dt_Agendamento { get; set; }
          [Display(Name = "Marcado")]
        public DateTime? dt_Marcacao { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual User User { get; set; }
        public virtual Clinica Clinica { get; set; }
    }
}

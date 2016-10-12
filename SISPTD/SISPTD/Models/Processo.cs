namespace SISPTD.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Processo")]
    public  class Processo
    {
       [Key]
        public long processoId { get; set; }
        public System.DateTime dtCadastro { get; set; }

        public string Procedimento { get; set; }
        public string Clinica { get; set; }
        [Display(Name = "Cid")]
        public long? cidId { get; set; }
        public string CaraterInternacao { get; set; }
        [Display(Name="Observações")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = " Observações é Campo Obrigatorio! ")]
        public string observacoes { get; set; }
       
        //public long pessoaId { get; set; }
        public long movimentacaoId { get; set; }
        public long? agendamentoId { get; set; }
        public long? pacienteId { get; set; }

        public long periciaId { get; set; }

        public long ? medicoId { get; set; }
        public virtual Agendamento Agendamento { get; set; }
        public virtual Pericia Pericia { get; set; }
        public virtual Pessoa Paciente { get; set; }
        public virtual Pessoa Medico { get; set; }
        public virtual Cid Cid { get; set; }

        public virtual ICollection<Movimentacao> ListaDeMovimentacao { get; set; }

    }
}

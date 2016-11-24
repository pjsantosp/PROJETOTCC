namespace SISPTD.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public  class Processo
    {
        public long processoId { get; set; }
        public System.DateTime dtCadastro { get; set; }

        public string Procedimento { get; set; }
        public string Setor { get; set; }
        public long ? Usuario { get; set; }


        public string Clinica { get; set; }
        [Display(Name = "Cid")]
        public long? cidId { get; set; }
        public string CaraterInternacao { get; set; }
        [Display(Name="Observa��es")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = " Observa��es � Campo Obrigatorio! ")]
        public string observacoes { get; set; }


        public long movimentacaoId { get; set; }
        public long? agendamentoId { get; set; }
        public long? periciaId { get; set; }


        public long? pacienteId { get; set; }


        public long ? medicoId { get; set; }

        public virtual ICollection<Agendamento> listaAgendamento { get; set; }
        public virtual ICollection<Pericia> listaPericia { get; set; }
        public virtual Pessoa Paciente { get; set; }
        public virtual Pessoa Medico { get; set; }
        public virtual Cid Cid { get; set; }

        public virtual ICollection<Movimentacao> ListaDeMovimentacao { get; set; }

    }
}

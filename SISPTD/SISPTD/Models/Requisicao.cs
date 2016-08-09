namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Requisicao")]
    public class Requisicao
    {
        public Requisicao()
        {
            //this.PessoaAcompanhante = new HashSet<Pessoa>();
            this.PessoaAcompanhante = new List<Pessoa>(); 

        }
        [Key]
        public long requisicaoId { get; set; }
        public long? pacienteId { get; set; }
        public long? usuarioId { get; set; }
        public int? IdCidadesOrigem { get; set; }
        public int IdCidadesDestino { get; set; }
        [Display(Name = "Data da Requisição")]
        public DateTime dtRequisicao { get; set; }

        [StringLength(200)]
        [Display(Name = "Observações")]
        public string observacoes { get; set; }

        public virtual Via Via { get; set; }
        public virtual Trecho Trecho { get; set; }
        public virtual Cidades CidadeOrigem { get; set; }
        public virtual Cidades CidadeDestino { get; set; }
        public virtual List<Pessoa> PessoaAcompanhante { get; set; }
        public virtual Pessoa Paciente { get; set; }
        public virtual User Usuario { get; set; }
    }
}

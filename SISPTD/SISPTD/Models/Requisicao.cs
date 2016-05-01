namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Requisicao")]
    public class Requisicao
    {
        [Key]
        public long requisicaoId { get; set; }
        public long? pacienteId { get; set; }
        public long? usuarioId { get; set; }
        public int? IdCidadesOrigem { get; set; }
        public int IdCidadesDestino { get; set; }
        public DateTime dtRequisicao { get; set; }

        [StringLength(200)]
        [Display(Name = "Observações")]
        public string observacoes { get; set; }

        public virtual Via via { get; set; }
        public virtual Trecho trecho { get; set; }
        public virtual Cidades CidadeOrigem { get; set; }
        public virtual Cidades CidadeDestino { get; set; }
        public virtual ICollection<Pessoa> PessoaAcompanhante { get; set; }
        public virtual Pessoa Paciente { get; set; }
        public virtual User Usuario { get; set; }
    }
}

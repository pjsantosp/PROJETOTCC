namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Requisicao")]
    public class Requisicao
    {
        [Key]
        public long requisicaoId { get; set; }

        public long? usuarioId { get; set; }
        public long pessoaId { get; set; }
        public int IdCidadeOrigem { get; set; }
        public int? IdCidadeDestino { get; set; }

        [StringLength(200)]
        public string observacoes { get; set; }

        public virtual ICollection<ItemRequisicao> ItemRequisicao { get; set; }

        public virtual Cidades CidadeOrigem { get; set; }
        public virtual Cidades CidadeDestino { get; set; }
        public virtual User User { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual Trecho trecho { get; set; }
    }
}

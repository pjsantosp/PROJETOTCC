namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    public  class ItemRequisicao
    {
        [Key]
        public long itemId { get; set; }

        public long pessoaId { get; set; }

        public long? requisicaoId { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual Requisicao Requisicao { get; set; }

       
    }
}

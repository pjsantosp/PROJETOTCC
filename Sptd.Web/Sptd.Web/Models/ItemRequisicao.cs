namespace Sptd.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ItemRequisicao")]
    public partial class ItemRequisicao
    {
        [Key]
        public long itemId { get; set; }

        public long fk_Pessoa { get; set; }

        public long? fk_Requisicao { get; set; }

        public long fk_Trecho { get; set; }
        public long fk_Trecho1 { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual Requisicao Requisicao { get; set; }

        public virtual Trecho Trecho { get; set; }
        public virtual Trecho Trecho1 { get; set; }
    }
}

namespace Sptd.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Trecho")]
    public partial class Trecho
    {
        public Trecho()
        {
            ItemRequisicao = new HashSet<ItemRequisicao>();
        }

        public long trechoId { get; set; }

        [MaxLength(12)]
        public byte[] descricao { get; set; }

        public virtual ICollection<ItemRequisicao> ItemRequisicao { get; set; }
    }
}

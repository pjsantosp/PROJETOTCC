namespace Sptd.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Requisicao")]
    public partial class Requisicao
    {
        public Requisicao()
        {
            ItemRequisicao = new HashSet<ItemRequisicao>();
        }

        public long requisicaoId { get; set; }

        public long? fk_UsuarioId { get; set; }

        public byte[] observacoes { get; set; }

        public virtual ICollection<ItemRequisicao> ItemRequisicao { get; set; }

        public virtual User User { get; set; }
    }
}

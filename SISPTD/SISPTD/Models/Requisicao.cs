namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

   
    public  class Requisicao
    {

        public long requisicaoId { get; set; }

        public long? usuarioId { get; set; }

        public byte[] observacoes { get; set; }

        public virtual ICollection<ItemRequisicao> ItemRequisicao { get; set; }

        public virtual User User { get; set; }
    }
}

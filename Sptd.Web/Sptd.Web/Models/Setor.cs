namespace Sptd.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Setor")]
    public partial class Setor
    {
        public Setor()
        {
            DistribProcesso = new HashSet<DistribProcesso>();
            DistribProcesso1 = new HashSet<DistribProcesso>();
        }

        public long setorId { get; set; }

        [StringLength(25)]
        public string descricao { get; set; }

        public virtual ICollection<DistribProcesso> DistribProcesso { get; set; }

        public virtual ICollection<DistribProcesso> DistribProcesso1 { get; set; }
    }
}

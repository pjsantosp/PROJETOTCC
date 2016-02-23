namespace Sptd.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cid")]
    public partial class Cid
    {
        public Cid()
        {
            Pericia = new HashSet<Pericia>();
        }

        public long cidId { get; set; }

        [StringLength(5)]
        public string codigoCid { get; set; }

        [StringLength(150)]
        public string descricao { get; set; }

        public virtual ICollection<Pericia> Pericia { get; set; }
    }
}

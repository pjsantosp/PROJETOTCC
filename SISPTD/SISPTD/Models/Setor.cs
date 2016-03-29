namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Setor")]
    public  class Setor
    {
        [Key]
        public long setorId { get; set; }

        [StringLength(25)]
        [Display(Name="Nome do Setor")]
        public string descricao { get; set; }

        public virtual ICollection<DistribProcesso> DistribProcesso { get; set; }

        public virtual ICollection<DistribProcesso> DistribProcesso1 { get; set; }
    }
}

namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

   
    public  class Pericia
    {
       [Key]
        public long periciaId { get; set; }

        [Required]
        public string descricao { get; set; }

        public long? cidId { get; set; }

        public DateTime dt_Pericia { get; set; }

        public long medicoId { get; set; }

        public int? situacao { get; set; }

        public long pessoaId { get; set; }

        public virtual Cid Cid { get; set; }

        public virtual ICollection<DistribProcesso> DistribProcesso { get; set; }

        public virtual Medico Medico { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}

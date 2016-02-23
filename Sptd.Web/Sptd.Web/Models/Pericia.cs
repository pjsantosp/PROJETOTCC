namespace Sptd.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pericia")]
    public partial class Pericia
    {
        public Pericia()
        {
            DistribProcesso = new HashSet<DistribProcesso>();
        }

        public long periciaId { get; set; }

        [Required]
        public string descricao { get; set; }

        public long? fk_CidId { get; set; }

        public DateTime dt_Pericia { get; set; }

        public long fk_MedicoId { get; set; }

        public int? situacao { get; set; }

        public long fk_PessoaId { get; set; }

        public virtual Cid Cid { get; set; }

        public virtual ICollection<DistribProcesso> DistribProcesso { get; set; }

        public virtual Medico Medico { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}

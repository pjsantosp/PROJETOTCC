namespace Sptd.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Medico")]
    public partial class Medico
    {
        public Medico()
        {
            Pericia = new HashSet<Pericia>();
        }

        public long medicoId { get; set; }

        public long? fk_Especialidade { get; set; }

        public long fk_PessoaId { get; set; }

        [Required]
        [StringLength(20)]
        public string crm_Medico { get; set; }

        public virtual Especialidade Especialidade { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual ICollection<Pericia> Pericia { get; set; }
    }
}

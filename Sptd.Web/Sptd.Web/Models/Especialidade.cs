namespace Sptd.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Especialidade")]
    public partial class Especialidade
    {
        public Especialidade()
        {
            Medico = new HashSet<Medico>();
        }

        public long EspecialidadeId { get; set; }

        [StringLength(50)]
        public string descricao { get; set; }

        public virtual ICollection<Medico> Medico { get; set; }
    }
}

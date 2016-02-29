namespace SISPTD.Models
{
    
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Especialidade")]
    public class Especialidade
    {
        [Key]
        public long EspecialidadeId { get; set; }

        [StringLength(50)]
        public string descricao { get; set; }

        public virtual ICollection<Medico> Medico { get; set; }
    }
}

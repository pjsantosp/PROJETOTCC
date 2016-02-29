namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Medico")]
    public class Medico
    {
        [Key]
        public long medicoId { get; set; }

        public long? especialidadeId { get; set; }

        public long pessoaId { get; set; }

        [Required]
        [StringLength(20)]
        public string crm_Medico { get; set; }

        public virtual Especialidade Especialidade { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual ICollection<Pericia> Pericia { get; set; }
    }
}

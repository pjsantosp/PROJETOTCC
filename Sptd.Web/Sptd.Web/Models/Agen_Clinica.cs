namespace Sptd.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agen_Clinica
    {
        [Key]
        public long Agen_CliniciaId { get; set; }

        public long? fk_Clinica { get; set; }

        public long? fk_Agendamento { get; set; }

        public virtual Agendamento Agendamento { get; set; }

        public virtual Clinica Clinica { get; set; }
    }
}

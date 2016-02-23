namespace Sptd.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Agendamento")]
    public partial class Agendamento
    {
        public Agendamento()
        {
            Agen_Clinica = new HashSet<Agen_Clinica>();
        }

        public long agendamentoId { get; set; }

        public DateTime? dt_Agendamento { get; set; }

        public DateTime? dt_Marcacao { get; set; }

        public long? fk_Pessoa { get; set; }

        public long? fk_Usuario { get; set; }

        public virtual ICollection<Agen_Clinica> Agen_Clinica { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual User User { get; set; }
    }
}

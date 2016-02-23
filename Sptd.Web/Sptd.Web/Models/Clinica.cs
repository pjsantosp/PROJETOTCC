namespace Sptd.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Clinica")]
    public partial class Clinica
    {
        public Clinica()
        {
            Agen_Clinica = new HashSet<Agen_Clinica>();
        }

        public long clinicaId { get; set; }

        [StringLength(150)]
        public string nome_Clinica { get; set; }

        public int? fk_Cidade { get; set; }

        [StringLength(10)]
        public string tel_Clinica { get; set; }

        public virtual ICollection<Agen_Clinica> Agen_Clinica { get; set; }

        public virtual Cidades Cidades { get; set; }
    }
}

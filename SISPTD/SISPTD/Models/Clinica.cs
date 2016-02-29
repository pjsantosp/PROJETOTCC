namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Clinica")]
    public  class Clinica
    {
       [Key]
        public long clinicaId { get; set; }

        [StringLength(150)]
        public string nome_Clinica { get; set; }

        public int? IdCidade { get; set; }

        [StringLength(10)]
        public string tel_Clinica { get; set; }

        public virtual Cidades Cidades { get; set; }
    }
}

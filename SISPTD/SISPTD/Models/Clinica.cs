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
        [Display(Name="Clinica")]
        [StringLength(150)]
        public string nome_Clinica { get; set; }
        [Display(Name="Cidade")]
        public int? IdCidade { get; set; }
        [Display(Name="Telefone")]
        [StringLength(10)]
        public string tel_Clinica { get; set; }

        public virtual Cidades Cidades { get; set; }
        public virtual ICollection<Agendamento> ListadeAgendamento { get; set; }
    }
}

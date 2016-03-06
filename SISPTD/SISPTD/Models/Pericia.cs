namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

   
    public  class Pericia
    {
       [Key]
        public long periciaId { get; set; }

        [Required, Display(Name="Descrição")]
        public string descricao { get; set; }
        [Display(Name="Cid")]
        public long? cidId { get; set; }
        [Display(Name="Data da Pericia")]
        public DateTime dt_Pericia { get; set; }
        [Display(Name="Medico")]
        public long medicoId { get; set; }
        [Display(Name="Situação")]
        [DataType(DataType.MultilineText)]
        
        public string situacao { get; set; }
        [Display(Name="Paciente")]
        public long pessoaId { get; set; }

        public virtual Cid Cid { get; set; }

        public virtual Medico Medico { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}

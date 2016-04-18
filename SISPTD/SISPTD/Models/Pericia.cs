namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Pericia
    {
        [Key]
        public long periciaId { get; set; }

        [Required, Display(Name = "Descrição")]
        public string descricao { get; set; }
        [Display(Name = "Cid")]
        public long? cidId { get; set; }
        [Display(Name = "Data da Pericia")]
        public DateTime dt_Pericia { get; set; }

        [Display(Name = "Situação")]
        [DataType(DataType.MultilineText)]

        public string situacao { get; set; }

        public long? pacientePessoaId { get; set; }

        public virtual Cid Cid { get; set; }

        public virtual ICollection<Pessoa> Medico { get; set; }

        public virtual Pessoa Paciente { get; set; }

        public virtual TipoPericia TipoPericia { get; set; }
    }
}

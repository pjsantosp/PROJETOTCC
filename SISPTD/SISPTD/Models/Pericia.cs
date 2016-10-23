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
        [DataType(DataType.MultilineText)]
        public string descricao { get; set; }
        [Display(Name = "Cid")]
        public long? cidId { get; set; }
        [Display(Name = "Data da Pericia")]
        public DateTime dt_Pericia { get; set; }

        public long? medicoPessoaId { get; set; }
        public virtual Cid Cid { get; set; }

        public virtual Pessoa Medico { get; set; }
        public virtual Processo Processo { get; set; }

        public virtual TipoPericia TipoPericia { get; set; }
        public virtual Situacao Situacao { get; set; }
    }
}

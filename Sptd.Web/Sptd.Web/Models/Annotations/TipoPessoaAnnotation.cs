using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Sptd.Web.Models.Annotations
{
    public class TipoPessoaAnnotation
    {
        [Key]
        public long tipo_PessoaId { get; set; }
        [Display(Name="Descrição")]
        [DataType(DataType.MultilineText)]
        public string descricao { get; set; }

        public virtual ICollection<Pessoa> Pessoa { get; set; }
    }
}
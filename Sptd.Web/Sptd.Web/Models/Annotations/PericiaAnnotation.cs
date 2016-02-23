using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sptd.Web.Models.Annotations
{
    public class PericiaAnnotation
    {
        
        
        public long periciaId { get; set; }
        [Display(Name="Descrição")]
        public string descricao { get; set; }
        [Display(Name="Cid")]
        public Nullable<long> fk_CidId { get; set; }
        [Display(Name="Data da Pericia")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=false)]
        public System.DateTime dt_Pericia { get; set; }
        [Display(Name="Médico")]
        public long fk_MedicoId { get; set; }
        public long fk_PessoaId { get; set; }

        public virtual Cid Cid { get; set; }
        public virtual Medico Medico { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
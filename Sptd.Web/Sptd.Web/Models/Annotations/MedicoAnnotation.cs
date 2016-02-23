using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Sptd.Web.Models.Annotations
{
    public class MedicoAnnotation
    {
       
        public long medicoId { get; set; }
        public long fk_PessoaId { get; set; }
        [Display(Name="CRM")]
        [Required(ErrorMessage="Campo de Preenchimento Obrigatório")]
        public string crm_Medico { get; set; }

        public virtual Pessoa Pessoa { get; set; }
        public virtual ICollection<Pericia> Pericia { get; set; }
    }
}
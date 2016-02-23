using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sptd.Web.Models.Annotations
{
    public class AgendamentoAnnotation
    {
       
        public long agendamentoId { get; set; }
        [Display(Name="Data Agendamento")]
        [Required(ErrorMessage="Campo de Preenchimento Obrigatório")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=true)]
        public Nullable<System.DateTime> dt_Agendamento { get; set; }
        [Display(Name="Data da Marcação")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=false)]
        public Nullable<System.DateTime> dt_Marcacao { get; set; }
        public Nullable<long> fk_Pessoa { get; set; }
        public Nullable<long> fk_Usuario { get; set; }
        //public Nullable<long> fk_Clinica { get; set; }
        public virtual ICollection<Agen_Clinica> Agen_Clinica { get; set; }
        public virtual Clinica Clinica { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual User User { get; set; }
    }
}
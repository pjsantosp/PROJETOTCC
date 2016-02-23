using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Sptd.Web.Models.Annotations
{
    public class ClinicaAnnotation
    {
       
        public long clinicaId { get; set; }
        [Display(Name="Clinica")]
        public string nome_Clinica { get; set; }
        [Display(Name="Telefone")]
        [Required(ErrorMessage="Campo de Preenchimento Obrigatório")]
        public string tel_Clinica { get; set; }

        public virtual ICollection<Agendamento> Agendamento { get; set; }
        public virtual Cidades Cidades { get; set; }
    }
}
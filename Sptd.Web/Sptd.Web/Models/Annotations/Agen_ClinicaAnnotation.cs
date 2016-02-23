using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sptd.Web.Models.Annotations
{
    public class Agen_ClinicaAnnotation
    {
        public long Agen_CliniciaId { get; set; }
        public Nullable<long> fk_Clinica { get; set; }
        public Nullable<long> fk_Agendamento { get; set; }

        public virtual Agendamento Agendamento { get; set; }
        public virtual Clinica Clinica { get; set; }
    }
}
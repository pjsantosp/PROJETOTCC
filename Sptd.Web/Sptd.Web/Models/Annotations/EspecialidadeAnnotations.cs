using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sptd.Web.Models.Annotations
{
    public class EspecialidadeAnnotations
    {
        public long EspecialidadeId { get; set; }
        public string descricao { get; set; }

        public virtual ICollection<Medico> Medico { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sptd.Web.Models.Annotations
{
    public class EstadoAnnotation
    {
        [Key]
        public int IdEstado { get; set; }
        public string Estado1 { get; set; }
        public string Sigla { get; set; }

        public virtual ICollection<Cidades> Cidades { get; set; }
    }
}
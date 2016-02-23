using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sptd.Web.Models
{
    public class CidadesAnnotation
    {
        public int IdCidade { get; set; }
        public string Cidade { get; set; }
        public int IdEstado { get; set; }

        public virtual Estado Estado { get; set; }
        public virtual ICollection<Endereco> Endereco { get; set; }
    }
}
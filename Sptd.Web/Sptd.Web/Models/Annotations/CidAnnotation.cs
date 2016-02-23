using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sptd.Web.Models
{
    public class CidAnnotation
    {
        
        public long cidId { get; set; }
        [Display(Name="Código do CID")]
        public string codigoCid { get; set; }
        [Display(Name="Descrição")]
        public string descricao { get; set; }
        public virtual ICollection<Pericia> Pericia { get; set; }
    }
}
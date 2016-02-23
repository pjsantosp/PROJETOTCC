using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sptd.Web.Models.Annotations
{
    public class SetorAnnotation
    {
       
        public long setorId { get; set; }
        [Display(Name="Descrição")]
        [DataType(DataType.MultilineText)]
        public string descricao { get; set; }
    }
}
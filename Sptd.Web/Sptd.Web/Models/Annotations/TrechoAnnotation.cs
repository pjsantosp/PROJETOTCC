using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Sptd.Web.Models.Annotations
{
    public class TrechoAnnotation
    {
        [Key]
        public long trechoId { get; set; }
        [Display(Name="Descrição")]
        [DataType(DataType.MultilineText)]
        public byte[] descricao { get; set; }
    }
}
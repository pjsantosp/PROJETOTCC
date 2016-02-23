using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sptd.Web.Models.Annotations
{
    public class ItemRequisicaoAnnotation
    {
        
        public long itemId { get; set; }
        public Nullable<long> fk_RequisicaoId { get; set; }
    }
}
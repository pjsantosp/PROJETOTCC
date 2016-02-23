using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Sptd.Web.Models.Annotations
{
    public class RequisicaoAnnotation
    {
        
        public long requisicaoId { get; set; }
        [Display(Name="Usuário")]
        public Nullable<long> fk_UsuarioId { get; set; }
        public long fk_Pessoa { get; set; }
        [Display(Name="Observações")]
        [DataType(DataType.MultilineText)]
        public byte[] observacoes { get; set; }
        [Display(Name="Trecho")]
        public long fk_Trecho { get; set; }
        public long fk_ItemId { get; set; }

        public virtual Pessoa Pessoa { get; set; }
        public virtual User User { get; set; }
    }
}
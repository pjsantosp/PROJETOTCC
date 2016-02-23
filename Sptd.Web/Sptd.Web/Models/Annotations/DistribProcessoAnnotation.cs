using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sptd.Web.Models
{
    public class DistribProcessoAnnotation
    {
        
        public long distrib_ProcessoId { get; set; }
        [Display(Name="Origem")]
        public string origem { get; set; }
        [Display(Name="Destino")]
        public string destino { get; set; }
        [Display(Name="Data de Envio")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=false)]
        public System.DateTime dt_Envio { get; set; }
        [Display(Name="Data de Recebimento")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=false)]
        public System.DateTime dt_Recebimento { get; set; }
        [Display(Name="Observações")]
        public string observacoes { get; set; }
        public long fk_PessoaId { get; set; }
        public long fk_UsuarioId { get; set; }

        public virtual Pessoa Pessoa { get; set; }
        public virtual User User { get; set; }
    }
}
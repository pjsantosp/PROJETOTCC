using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Sptd.Web.Models.Annotations
{
    public class EnderecoAnnotation
    {
       
        public long enderecoId { get; set; }
        [Display(Name="Cidade")]
        public long fk_CidadeId { get; set; }
        [Display(Name="Rua")]
        public string rua { get; set; }
        [Display(Name="Numero")]
        public string numero { get; set; }
        [Display(Name="Cep")]
        public string cep { get; set; }
        [Display(Name="Bairro")]
        public string bairro { get; set; }
        public virtual Cidades Cidades { get; set; }
        public virtual ICollection<Pessoa> Pessoa { get; set; }
    }
}
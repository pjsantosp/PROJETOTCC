using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sptd.Web.Models.Annotations
{
    public class UserAnnotation
    {
        [Key]
        public long usuarioId { get; set; }
        [Display(Name="Login")]
        [Required(ErrorMessage="Campo Login é Obrigatório")]
        public string login { get; set; }
        [Display(Name="Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage="Campo Senha é Obrigatório")]
    
        public string senha { get; set; }
        public long fk_PessoaId { get; set; }

        public virtual ICollection<Agendamento> Agendamento { get; set; }
        public virtual ICollection<DistribProcesso> DistribProcesso { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual ICollection<Requisicao> Requisicao { get; set; }
    }
}
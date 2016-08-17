using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SISPTD.Models
{
    public class PessoaRequisicao
    {
        [Key]
        public int pessoaRequisicaoId { get; set; }
        public long? pessoaId { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
        public long? requisicaoId { get; set; }
       
        public virtual Pessoa Pessoa { get; set; }
        public virtual Requisicao Requisicao { get; set; }
    }
}
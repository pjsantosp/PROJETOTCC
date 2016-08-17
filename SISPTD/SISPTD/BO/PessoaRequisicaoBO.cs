using SISPTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SISPTD.BO
{
    public class PessoaRequisicaoBO : CrudComumEntity<PessoaRequisicao, int>
    {
        public PessoaRequisicaoBO(DbContext contexto) : base(contexto)
        {
        }
       
    }
}
using SISPTD.Models;
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
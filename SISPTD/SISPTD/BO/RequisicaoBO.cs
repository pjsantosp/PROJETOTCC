using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class RequisicaoBO : CrudComumEntity<Requisicao, long>
    {
        public RequisicaoBO(DbContext contexto) 
            : base(contexto)
        {
        }

        public IEnumerable<Requisicao> ObterRequisicao()
        {
            try
            {
                IEnumerable<Requisicao> listarequisicao = _contexto.Set<Requisicao>()
               .Include(d => d.Paciente)
               .Include(d => d.PessoaAcompanhante).ToList();
               
                return listarequisicao;
            }
            catch (Exception e)
            {

                throw new Exception("Erro na busca na lista de pessoa", e);
            }

        }
    }
}
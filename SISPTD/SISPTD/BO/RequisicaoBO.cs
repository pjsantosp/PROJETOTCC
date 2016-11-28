using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SISPTD.Models;
using PagedList;

namespace SISPTD.BO
{
    public class RequisicaoBO : CrudComumEntity<Requisicao, long>
    {
        public RequisicaoBO(DbContext contexto) 
            : base(contexto)
        {
        }
        public IEnumerable<Requisicao> ObterRequisicao(int? pagina, int numPagina, string busca)
        {
            try
            {
                IEnumerable<Requisicao> listarequisicao = _contexto.Set<Requisicao>()
               .Include(d => d.Paciente).Where(d=> d.Paciente.cpf.Contains(busca) || d.Paciente.cns.Contains(busca))
               .ToList();
                return listarequisicao.OrderByDescending(s=>s.dtRequisicao).ToPagedList(pagina.Value, numPagina);
            }
            catch (Exception e)
            {
                throw new Exception("Erro na busca na lista de pessoa", e);
            }

        }
    }
}
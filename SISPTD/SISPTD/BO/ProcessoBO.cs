using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;
using System.Data.Entity;
using PagedList;




namespace SISPTD.BO
{
    public class ProcessoBO : CrudComumEntity<Processo, long>
    {
        public ProcessoBO(dbSISPTD contexto)
            : base(contexto)
        {

        }
        public override void Inserir(Processo entidade)
        {
            if (entidade.pacienteId == 0)
                throw new Exception("É necessario um Paciente!");
            //if (entidade. == entidade.SetorDestinoId)
            //    throw new Exception("Setor de Origem e Destino, não pode ser iguais");
            base.Inserir(entidade);

        }
        public IEnumerable<Processo> ObterProcesso(string busca, int? pagina, int tamanhoPagina)
        {
            try
            {
                IEnumerable<Processo> listaProcesso = _contexto.Set<Processo>()
               .Include(d => d.Paciente)
               .Where(x => x.Paciente.cpf.Contains(busca) || x.Paciente.cns.Contains(busca));
                return listaProcesso.OrderByDescending(p=> p.dtCadastro).ToPagedList(pagina.Value, tamanhoPagina);
            }
            catch (Exception e)
            {

                throw new Exception("Erro na busca na lista de pessoa no Processo", e);
            }

        }
       
    }
}
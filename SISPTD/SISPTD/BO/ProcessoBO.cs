using System;
using System.Collections.Generic;
using System.Linq;
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
        private SetorBO setorBO = new SetorBO(new dbSISPTD());
        public override void Inserir(Processo entidade)
        {
            if (entidade.pacienteId == 0)
                throw new Exception("É necessario um Paciente!");
            
            base.Inserir(entidade);

        }
        public void AlteraUsuarioDoProcesso(long usuario, long processoId)
        {
            try
            {
                Processo objProcesso = _contexto.Set<Processo>().Find(processoId);
                objProcesso.Usuario = usuario;
                base.Alterar(objProcesso);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Processo> ObterProcesso(string busca, int? pagina, int tamanhoPagina)
        {
            try
            {
                IEnumerable<Processo> listaProcesso = _contexto.Set<Processo>()
               .Include(d => d.Paciente)
               .Where(x => x.Paciente.cpf.Contains(busca) || x.Paciente.cns.Contains(busca));
                return listaProcesso.OrderByDescending(p => p.dtCadastro).ToPagedList(pagina.Value, tamanhoPagina);
            }
            catch (Exception e)
            {
                throw new Exception("Erro na busca na lista de pessoa no Processo", e);
            }

        }
        public IEnumerable<Processo> ObterAgendamento(int? pagina, int tamanhoPagina)
        {
            IEnumerable<Processo> listaDePericia = _contexto.Set<Processo>()
                .Include(p => p.Paciente)
                .Where(m => m.Setor =="AGENDAMENTO");
            return listaDePericia.OrderByDescending(m => m.dtCadastro).ToPagedList(pagina.Value, tamanhoPagina);
        }
        public IEnumerable<Processo> ObterPericias(int? pagina, int tamanhoPagina)
        {
            IEnumerable<Processo> listaDePericia = _contexto.Set<Processo>()
                .Include(p => p.Paciente)
                .Where(m => m.Setor =="PERICIA");
            return listaDePericia.OrderByDescending(m => m.dtCadastro).ToPagedList(pagina.Value, tamanhoPagina);
        }
        public List<Processo> ObterAcompanhantes(int? pessoaId)
        {
            List<Processo> listaDeAcompanhantes = _contexto.Set<Processo>()
                .Include(p => p.ListaDeAcompantesProcesso)
                .Where(p => p.pacienteId == pessoaId).ToList();
            return listaDeAcompanhantes;
        }

    }
}
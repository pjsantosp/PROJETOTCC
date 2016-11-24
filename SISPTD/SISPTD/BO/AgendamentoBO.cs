using SISPTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PagedList;

namespace SISPTD.BO
{
    public class AgendamentoBO : CrudComumEntity<Agendamento, long>
    {
        public AgendamentoBO(dbSISPTD contexto)
            : base(contexto)
        {

        }

        public IEnumerable<Agendamento> ObterAgendamento(int? pagina, int tamahopagina )
        {

            IEnumerable<Agendamento> listaDeAgendamento = _contexto.Set<Agendamento>()
             .Include(a => a.Processo)
             .Where(a=> a.Processo.Paciente.TipoPessoa == TipoPessoa.Paciente);

            return listaDeAgendamento.OrderByDescending(a => a.dt_Agendamento).ToPagedList(pagina.Value, tamahopagina);
        }
        public Processo ObterProcessoAgd(long? pacienteId)
        {
            try
            {
                Processo objProcesso = _contexto.Set<Processo>()
                    .Include(p => p.Paciente)
                    .Include(p => p.ListaDeMovimentacao)
                    .Include(p => p.listaAgendamento)
                    .FirstOrDefault(p => p.pacienteId.Value == pacienteId);
                return objProcesso;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool VerificaAgendamento(Agendamento agendamento)
        {
            try
            {
                var existe = _contexto.Set<Agendamento>().FirstOrDefault(a => a.dt_Agendamento == agendamento.dt_Agendamento && a.processoId == agendamento.processoId);
                if (existe != null)
                {
                    return true;

                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

      

    }
}
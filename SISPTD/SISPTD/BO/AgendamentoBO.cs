using SISPTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PagedList;

namespace SISPTD.BO
{
    public class AgendamentoBO:CrudComumEntity<Agendamento, long>
    {
        public AgendamentoBO(dbSISPTD contexto )
            :base(contexto)
        {

        }

        public IEnumerable<Agendamento> ObterAgendamento(int? pagina, int tamahopagina)
        {
            //var ListaDePericias =( from a in _contexto.Set<Agendamento>()
            //                      join proc in _contexto.Set<Processo>() on a.processoId equals proc.processoId
            //                      join p in _contexto.Set<Pessoa>() on proc.processoId equals p.pessoaId
            //                      select new
            //                      {
            //                          _paciente = p.nome,
            //                          _data = a.dt_Agendamento,
            //                          _dataMarcacao = a.dt_Agendamento


            //                      }).ToList();
           IEnumerable<Agendamento> listaDeAgendamento = _contexto.Set<Agendamento>()
            .Include(a => a.Processo);

            return listaDeAgendamento.OrderByDescending(a => a.dt_Agendamento).ToPagedList(pagina.Value, tamahopagina);
        }

    }
}
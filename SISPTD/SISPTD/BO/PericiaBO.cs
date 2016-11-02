using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using SISPTD.Models;
using System.Collections;
using PagedList;

namespace SISPTD.BO
{
    public class PericiaBO:CrudComumEntity<Pericia, long>
    {
        public PericiaBO(dbSISPTD contexto)
            :base(contexto)
        {

        }


        public IEnumerable<Pericia> ObterPericia(int? pagina, int tamanhoPagina)
        {
            try
            {
                IEnumerable<Pericia> listaDePericia = _contexto.Set<Pericia>().Include(p => p.Processo);
                return listaDePericia.OrderByDescending(p => p.dt_Pericia).ToPagedList(pagina.Value, tamanhoPagina);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override void Inserir(Pericia entidade)
        {
            try
            {
                //if (String.IsNullOrEmpty(entidade.Medico.nome))
                //    throw new Exception("O Nome do Médico é obrigátorio!");
                //if (string.IsNullOrEmpty(entidade.Processo.Paciente.nome))
                //    throw new Exception("O processo contido na pericia não contém um paciente");
                    base.Inserir(entidade);
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao tentar inserir Pericia", ex);
            }
            
        }

    }
}
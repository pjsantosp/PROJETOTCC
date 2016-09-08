using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class CidadeBO: CrudComumEntity<Cidades, int>
    {
        public CidadeBO(dbSISPTD contexto)
            :base(contexto)
        {

        }
        public List<Cidades> ObterCidades()
        {
            try
            {
                return _contexto.Set<Cidades>().ToList();
            }
            catch (Exception e)
            {

                throw new Exception(" Erro durante a listagem da Cidade", e);
            }
        }
    }
}
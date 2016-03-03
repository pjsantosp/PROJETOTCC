using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class CidadeBO
    {
        private dbSISPTD db = new dbSISPTD();
        public List<Cidades> ObterCidades()
        {
            try
            {
                return db.Cidades.Where(c => c.Estado.Sigla == "RO").ToList();
            }
            catch (Exception e)
            {
                
                throw new Exception(" Erro durante a listagem da Cidade", e);
            }
        }
    }
}
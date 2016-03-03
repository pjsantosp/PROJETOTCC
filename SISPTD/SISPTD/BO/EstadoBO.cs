using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class EstadoBO
    {
        private dbSISPTD db = new dbSISPTD();
        public int UfPadrao()
        {
            try
            {
                int ufPadrao = db.Estado.Where(e => e.Sigla == "RO").First().IdEstado;
                return ufPadrao;
            }
            catch (Exception e )
            {

                throw new Exception("Erro durante a listagem do Estado Padrão",e); ;
            }
        }
    }
}
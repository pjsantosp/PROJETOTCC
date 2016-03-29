using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;




namespace SISPTD.BO
{
    public class DistribProcessoBO:CrudComumEntity<DistribProcesso, long>
    {
        public DistribProcessoBO(dbSISPTD contexto)
            :base(contexto)
        {

        }
      
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class MedicoBO:CrudComumEntity<Medico, long>
    {
        public MedicoBO(dbSISPTD contexto)
            :base(contexto)
        {

        }
      
    }
}
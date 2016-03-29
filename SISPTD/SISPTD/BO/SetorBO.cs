using SISPTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SISPTD.BO
{
    public class SetorBO:CrudComumEntity<Setor, long>
    {
        public SetorBO(dbSISPTD contexto)
            :base(contexto)
        {

        }
      

    }
}
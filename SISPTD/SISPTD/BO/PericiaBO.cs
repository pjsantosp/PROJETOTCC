using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class PericiaBO:CrudComumEntity<Pericia, long>
    {
        public PericiaBO(dbSISPTD contexto)
            :base(contexto)
        {

        }
       
    }
}
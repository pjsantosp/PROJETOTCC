using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class CidBO:CrudComumEntity<Cid, long>
    {
        public CidBO(dbSISPTD contexto)
            :base(contexto)
        {

        }


    }
}
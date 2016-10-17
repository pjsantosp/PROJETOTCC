using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using SISPTD.Models;
using System.Collections;

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
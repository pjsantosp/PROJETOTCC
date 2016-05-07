using SISPTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SISPTD.BO
{
    public class AgendamentoBO:CrudComumEntity<Agendamento, long>
    {
        public AgendamentoBO(dbSISPTD contexto )
            :base(contexto)
        {

        }

    }
}
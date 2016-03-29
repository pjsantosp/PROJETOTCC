using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;
namespace SISPTD.BO
{
    public class EspecialidadeBO:CrudComumEntity<Especialidade, long>
    {
        public EspecialidadeBO(dbSISPTD contexto)
            :base(contexto)
        {

        }
       
    }
}
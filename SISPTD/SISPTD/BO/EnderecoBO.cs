using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class EnderecoBO:CrudComumEntity<Endereco, long>
    {
        public EnderecoBO(dbSISPTD contexto)
            :base(contexto)
        {
        }
       

    }
}
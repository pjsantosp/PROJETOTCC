using SISPTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SISPTD.BO
{
    public class EnderecoBO : CrudComumEntity<Endereco, long>
    {
        public EnderecoBO(DbContext contexto) : base(contexto)
        {
        }
    }
}
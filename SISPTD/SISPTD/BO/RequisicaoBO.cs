using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class RequisicaoBO : CrudComumEntity<Requisicao, long>
    {
        public RequisicaoBO(DbContext contexto) 
            : base(contexto)
        {
        }

       
    }
}
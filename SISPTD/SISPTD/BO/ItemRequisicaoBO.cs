using SISPTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SISPTD.BO
{
    public class ItemRequisicaoBO : CrudComumEntity<ItemRequisicao, long>
    {
        public ItemRequisicaoBO(DbContext contexto)
           : base(contexto)
        {
        }

    }
}
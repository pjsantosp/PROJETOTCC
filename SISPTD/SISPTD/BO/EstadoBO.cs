using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class EstadoBO: CrudComumEntity<Estado, int>
    {
        public EstadoBO(DbContext contexto) : base(contexto)
        {
        }
    }
}
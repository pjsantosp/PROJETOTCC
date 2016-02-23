using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sptd.Web.Models;

namespace Sptd.Web.Repositories
{
    public class EstadoRepository
    {
        public long EstadoPadrao()
        {
            using (DbSPTD db = new DbSPTD())
            {
                long SiglaPadrao = db.Estado.Where(e => e.Sigla == "RO").First().IdEstado;
                return SiglaPadrao;
            }
        }
        public List<Estado> ObterEstados()
        {
            using (DbSPTD db = new DbSPTD())
            {
                return db.Estado.ToList();
            }
        }
    }
}
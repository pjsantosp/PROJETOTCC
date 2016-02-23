using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sptd.Web.Models;

namespace Sptd.Web.Repositories
{
    public class CidadeRepository
    {
        public List<Cidades> ObterCidades()
        {
            using (DbSPTD db = new DbSPTD())
            {
                return db.Cidades.ToList();
            }
        }
        public List<Cidades> CidadesRO(long idEstado)
        {
            using (DbSPTD db = new DbSPTD())
            {
                IList<Cidades> cidades = db.Cidades.Where(c => c.IdEstado == idEstado).ToList();
                return cidades.ToList();
            }
        }
    }
}
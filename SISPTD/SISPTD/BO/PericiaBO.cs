using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class PericiaBO
    {
        private dbSISPTD db = new dbSISPTD();

        public List<Pericia> ObterPericia()
        {
            try
            {
                var pericia = db.Pericia
                    .Include("Cid")
                    .Include("Medico")
                    .Include("Pessoa");
                return pericia.ToList();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public Pericia ObterPericia(long? id)
        {
            try
            {
                Pericia pericia = db.Pericia.Find(id);
                return pericia;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
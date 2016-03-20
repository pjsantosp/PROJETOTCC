using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;




namespace SISPTD.BO
{
    public class DistribProcessoBO
    {
        private dbSISPTD db = new dbSISPTD();
        public List<DistribProcesso> ObterProcesso()
        {
            try
            {
                var distribProcesso = db.DistribProcesso
                .Include("Pessoa")
                .Include("SetorDestino")
                .Include("SetorOrigem")
                .Include("UserEnviou")
                .Include("UserRecebeu");
                return distribProcesso.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DistribProcesso ObterProcesso(long? id)
        {
            try
            {
                DistribProcesso distribProcesso = db.DistribProcesso.Find(id);
                return distribProcesso;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DistribProcesso AtualizarProcesso(DistribProcesso distribProcesso)
        {
            try
            {
                db.Entry(distribProcesso).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return distribProcesso;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Excluir(long id)
        {
            try
            {
                DistribProcesso distribProcesso = db.DistribProcesso.Find(id);
                db.DistribProcesso.Remove(distribProcesso);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
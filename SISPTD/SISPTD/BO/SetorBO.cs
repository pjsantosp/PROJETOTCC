using SISPTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SISPTD.BO
{
    public class SetorBO
    {
        private dbSISPTD db = new dbSISPTD();
        public List<Setor> ObterSetor()
        {
            try
            {
                return db.Setor.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro durante a listagem de Setor", e);
            }
        }
        public Setor ObterSetor(long? id)
        {
            try
            {
                Setor setor = db.Setor.Find(id);
                return setor;
            }
            catch (Exception e)
            {

                throw new Exception("Erro durante a busca do Setor", e);
            }
        }
        public Setor CriarSetor(Setor setor)
        {
            try
            {
                db.Setor.Add(setor);
                db.SaveChanges();
                return setor;
            }
            catch (Exception e)
            {

                throw new Exception("Erro durante a Criação do Setor", e);
            }
        }
        public Setor AtualizarSetor(Setor setor)
        {
            try
            {
                db.Entry(setor).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return setor;
            }
            catch (Exception e)
            {

                throw new Exception("Erro Durante a Atualização do Setor", e);
            }
        }
        public void Excluir(long id)
        {
            try
            {
                Setor setor = db.Setor.Find(id);
                db.Setor.Remove(setor);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
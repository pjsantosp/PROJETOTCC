using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;
namespace SISPTD.BO
{
    public class EspecialidadeBO
    {
        private dbSISPTD db = new dbSISPTD();
        public List<Especialidade> ObterEspecialidade()
        {
            try
            {
                return db.Especialidade.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Especialidade ObterEspecialidade(long? id)
        {
            try
            {
                Especialidade especialidade = db.Especialidade.Find(id);
                return especialidade;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Especialidade CriarEspecialidade(Especialidade especialidade)
        {
            try
            {
                db.Especialidade.Add(especialidade);
                db.SaveChanges();
                return especialidade;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Especialidade AtualizarEspecialidade(Especialidade especialidade)
        {
            try
            {
                db.Entry(especialidade).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return especialidade;
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
                Especialidade especialidade = db.Especialidade.Find(id);
                db.Especialidade.Remove(especialidade);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
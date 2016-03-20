using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class MedicoBO
    {
        dbSISPTD db = new dbSISPTD();
        public List<Medico> ObterMedico()
        {
            try
            {
                var medico = db.Medico
                .Include("Especialidade")
                .Include("Pessoa");
                return medico.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Medico ObterMedico(long? id)
        {
            try
            {
                Medico medico = db.Medico.Find(id);
                return medico;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Medico CriarMedico(Medico medico)
        {
            try
            {
                db.Medico.Add(medico);
                db.SaveChanges();
                return medico;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Medico AtualizarMedico(Medico medico)
        {
            try
            {
                db.Entry(medico).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return medico;
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
                Medico medico = db.Medico.Find(id);
                db.Medico.Remove(medico);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
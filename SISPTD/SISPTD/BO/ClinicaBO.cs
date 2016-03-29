using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class ClinicaBO: CrudComumEntity<Clinica, long>
    {
        public ClinicaBO(dbSISPTD contexto)
            :base(contexto)
        {

        }
        private dbSISPTD db = new dbSISPTD();
        public List<Clinica> ObterClinica()
        {
            try
            {
                var clinica = db.Clinica.Include("Cidades");
                return clinica.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Clinica ObterClinica(long? id)
        {
            try
            {
                Clinica clinica = db.Clinica.Find(id);
                return clinica;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Clinica CriarClinica(Clinica clinica)
        {
            try
            {
                db.Clinica.Add(clinica);
                db.SaveChanges();
                return clinica;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Clinica AtualizarClinica(Clinica clinica)
        {
            try
            {
                db.Entry(clinica).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return clinica;
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
                Clinica clinica = db.Clinica.Find(id);
                db.Clinica.Remove(clinica);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
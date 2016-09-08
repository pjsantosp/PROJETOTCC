using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SISPTD.Models;
using SISPTD.BO;
using PagedList;

namespace SISPTD.Controllers
{
    public class ClinicaController : Controller
    {
        private ClinicaBO clinicaBO = new ClinicaBO(new dbSISPTD());
        private CidadeBO cidadeBO = new CidadeBO(new dbSISPTD());

        // GET: Clinica
        public ActionResult Index(int? pagina)
        {
            int nPorPagina = 10;
            int numPagina = pagina ?? 1; 
            //ViewBag.IdCidade = new SelectList(cidadeBO.ObterCidades(), "IdCidade", "Cidade");
            return View(clinicaBO.Selecionar().OrderBy(x=> x.nome_Clinica).ToPagedList(numPagina, nPorPagina));
        }

        // GET: Clinica/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = clinicaBO.SelecionarPorId(id.Value);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            return View(clinica);
        }

        public ActionResult Create()
        {
           
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Clinica clinica)
        {
            if (ModelState.IsValid)
            {
                clinicaBO.Inserir(clinica);
                return RedirectToAction("Index");
            }

            ViewBag.IdCidade = new SelectList(cidadeBO.ObterCidades(), "IdCidade", "Cidade", clinica.IdCidade);
            return View(clinica);
        }

        // GET: Clinica/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = clinicaBO.SelecionarPorId(id.Value);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCidade = new SelectList(cidadeBO.ObterCidades(), "IdCidade", "Cidade", clinica.IdCidade);
            return View(clinica);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Clinica clinica)
        {
            if (ModelState.IsValid)
            {
                clinicaBO.Alterar(clinica);
                return RedirectToAction("Index");
            }
            ViewBag.IdCidade = new SelectList(cidadeBO.ObterCidades(), "IdCidade", "Cidade", clinica.IdCidade);
            return View(clinica);
        }

        // GET: Clinica/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = clinicaBO.SelecionarPorId(id.Value);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            return View(clinica);
        }

        // POST: Clinica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            clinicaBO.ExcluirPorId(id);
            return RedirectToAction("Index");
        }

       
    }
}

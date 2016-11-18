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

namespace SISPTD.Controllers
{
    public class EspecialidadeController : Controller
    {
        private EspecialidadeBO especialidadeBO = new EspecialidadeBO(new dbSISPTD());
        public ActionResult Index()
        {
            return View(especialidadeBO.Selecionar());
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = especialidadeBO.SelecionarPorId(id.Value);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                especialidadeBO.Inserir(especialidade);
                return RedirectToAction("Index");
            }

            return View(especialidade);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = especialidadeBO.SelecionarPorId(id.Value);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                especialidadeBO.Alterar(especialidade);
                return RedirectToAction("Index");
            }
            return View(especialidade);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = especialidadeBO.SelecionarPorId(id.Value);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            especialidadeBO.ExcluirPorId(id);
            return RedirectToAction("Index");
        }

       
    }
}

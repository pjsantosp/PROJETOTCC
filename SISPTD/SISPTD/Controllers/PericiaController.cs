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
    public class PericiaController : Controller
    {
        private dbSISPTD db = new dbSISPTD();
        private PericiaBO prBO = new PericiaBO();
        private PessoaBO pBO = new PessoaBO();

        // GET: Pericia
        public ActionResult Index()
        {
            
            return View(prBO.ObterPericia());
        }

        // GET: Pericia/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pericia pericia = prBO.ObterPericia(id);
            if (pericia == null)
            {
                return HttpNotFound();
            }
            return View(pericia);
        }

        // GET: Pericia/Create
        public ActionResult Create()
        {
            ViewBag.cidId = new SelectList(db.Cid, "cidId", "codigoCid");
            ViewBag.medicoId = new SelectList(db.Medico, "medicoId", "crm_Medico");
            ViewBag.pessoaId = new SelectList(pBO.ObterPessoa(), "pessoaId", "nome");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pericia pericia)
        {
            if (ModelState.IsValid)
            {
                db.Pericia.Add(pericia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cidId = new SelectList(db.Cid, "cidId", "codigoCid", pericia.cidId);
            ViewBag.medicoId = new SelectList(db.Medico, "medicoId", "crm_Medico", pericia.medicoId);
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", pericia.pessoaId);
            return View(pericia);
        }

        // GET: Pericia/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pericia pericia = db.Pericia.Find(id);
            if (pericia == null)
            {
                return HttpNotFound();
            }
            ViewBag.cidId = new SelectList(db.Cid, "cidId", "codigoCid", pericia.cidId);
            ViewBag.medicoId = new SelectList(db.Medico, "medicoId", "crm_Medico", pericia.medicoId);
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", pericia.pessoaId);
            return View(pericia);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "periciaId,descricao,cidId,dt_Pericia,medicoId,situacao,pessoaId")] Pericia pericia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pericia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cidId = new SelectList(db.Cid, "cidId", "codigoCid", pericia.cidId);
            ViewBag.medicoId = new SelectList(db.Medico, "medicoId", "crm_Medico", pericia.medicoId);
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", pericia.pessoaId);
            return View(pericia);
        }

        // GET: Pericia/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pericia pericia = db.Pericia.Find(id);
            if (pericia == null)
            {
                return HttpNotFound();
            }
            return View(pericia);
        }

        // POST: Pericia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Pericia pericia = db.Pericia.Find(id);
            db.Pericia.Remove(pericia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

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
        private PericiaBO periciaBO = new PericiaBO(new dbSISPTD());
        private PessoaBO pessoBO = new PessoaBO(new dbSISPTD());
        private MovimentacaoBO movimentacaoBO = new MovimentacaoBO(new dbSISPTD());
       

        // GET: Pericia
        public ActionResult Index()
        {
            
            return View(periciaBO.Selecionar());
        }

        

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pericia pericia = periciaBO.SelecionarPorId(id.Value);
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
            ViewBag.pacientePessoaId = new SelectList(pessoBO.Selecionar().Where(p=> p.tipo== 0), "pessoaId", "cpf");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "medicoPessoaId,descricao,cidId,situacao,pacientepessoaId")] Pericia pericia)
        {
            try
            {
                pericia.dt_Pericia = DateTime.Now;
                if (ModelState.IsValid)
                {
                    db.Pericia.Add(pericia);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                
            }
            catch (Exception ex)
            {

                TempData["Erro"] = "Ops ! " + ex.Message;
            }
            ViewBag.cidId = new SelectList(db.Cid, "cidId", "codigoCid", pericia.cidId);
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", pericia.Processo.Paciente.cpf);
            return View(pericia);
        }

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
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", pericia.Processo.Paciente.cpf);
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
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", pericia.Processo.Paciente.cpf);
            return View(pericia);
        }

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

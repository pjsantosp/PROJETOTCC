using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SISPTD.Models;

namespace SISPTD.Controllers
{
    public class AgendamentoController : Controller
    {
        private dbSISPTD db = new dbSISPTD();

        // GET: Agendamento
        public ActionResult Index()
        {
            var agendamento = db.Agendamento.Include(a => a.Pessoa).Include(a => a.User);
            return View(agendamento.ToList());
        }

        // GET: Agendamento/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamento agendamento = db.Agendamento.Find(id);
            if (agendamento == null)
            {
                return HttpNotFound();
            }
            return View(agendamento);
        }

        // GET: Agendamento/Create
        public ActionResult Create()
        {
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf");
            ViewBag.usuarioId = new SelectList(db.User, "usuarioId", "login");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                agendamento.dt_Marcacao = DateTime.Now;
                db.Agendamento.Add(agendamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", agendamento.pessoaId);
            ViewBag.usuarioId = new SelectList(db.User, "usuarioId", "login", agendamento.usuarioId);
            return View(agendamento);
        }

        // GET: Agendamento/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamento agendamento = db.Agendamento.Find(id);
            if (agendamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", agendamento.pessoaId);
            ViewBag.usuarioId = new SelectList(db.User, "usuarioId", "login", agendamento.usuarioId);
            return View(agendamento);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "agendamentoId,pessoaId,usuarioId,dt_Agendamento,dt_Marcacao")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agendamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", agendamento.pessoaId);
            ViewBag.usuarioId = new SelectList(db.User, "usuarioId", "login", agendamento.usuarioId);
            return View(agendamento);
        }

        // GET: Agendamento/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamento agendamento = db.Agendamento.Find(id);
            if (agendamento == null)
            {
                return HttpNotFound();
            }
            return View(agendamento);
        }

        // POST: Agendamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Agendamento agendamento = db.Agendamento.Find(id);
            db.Agendamento.Remove(agendamento);
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

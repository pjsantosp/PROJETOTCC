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
    public class DistribProcessoController : Controller
    {
        private dbSISPTD db = new dbSISPTD();

        // GET: DistribProcesso
        public ActionResult Index()
        {
            var distribProcesso = db.DistribProcesso.Include(d => d.Pericia).Include(d => d.Pessoa).Include(d => d.SetorDestino).Include(d => d.SetorOrigem).Include(d => d.UserEnviou).Include(d => d.UserRecebeu);
            return View(distribProcesso.ToList());
        }

        // GET: DistribProcesso/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistribProcesso distribProcesso = db.DistribProcesso.Find(id);
            if (distribProcesso == null)
            {
                return HttpNotFound();
            }
            return View(distribProcesso);
        }

        // GET: DistribProcesso/Create
        public ActionResult Create()
        {
            ViewBag.periciaId = new SelectList(db.Pericia, "periciaId", "descricao");
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "nome");
            ViewBag.SetorDestinoId = new SelectList(db.Setor, "setorId", "descricao");
            ViewBag.SetorOrigemId = new SelectList(db.Setor, "setorId", "descricao");
            ViewBag.usuarioEnviouId = new SelectList(db.User, "usuarioId", "login");
            ViewBag.usuarioRecebeuId = new SelectList(db.User, "usuarioId", "login");
            return View();
        }

        // POST: DistribProcesso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "distrib_ProcessoId,SetorOrigemId,SetorDestinoId,observacoes,pessoaId,usuarioEnviouId,usuarioRecebeuId,periciaId")] DistribProcesso distribProcesso)
        {
            if (ModelState.IsValid)
            {
                db.DistribProcesso.Add(distribProcesso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.periciaId = new SelectList(db.Pericia, "periciaId", "descricao", distribProcesso.periciaId);
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", distribProcesso.pessoaId);
            ViewBag.SetorDestinoId = new SelectList(db.Setor, "setorId", "descricao", distribProcesso.SetorDestinoId);
            ViewBag.SetorOrigemId = new SelectList(db.Setor, "setorId", "descricao", distribProcesso.SetorOrigemId);
            ViewBag.usuarioEnviouId = new SelectList(db.User, "usuarioId", "login", distribProcesso.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(db.User, "usuarioId", "login", distribProcesso.usuarioRecebeuId);
            return View(distribProcesso);
        }

        // GET: DistribProcesso/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistribProcesso distribProcesso = db.DistribProcesso.Find(id);
            if (distribProcesso == null)
            {
                return HttpNotFound();
            }
            ViewBag.periciaId = new SelectList(db.Pericia, "periciaId", "descricao", distribProcesso.periciaId);
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", distribProcesso.pessoaId);
            ViewBag.SetorDestinoId = new SelectList(db.Setor, "setorId", "descricao", distribProcesso.SetorDestinoId);
            ViewBag.SetorOrigemId = new SelectList(db.Setor, "setorId", "descricao", distribProcesso.SetorOrigemId);
            ViewBag.usuarioEnviouId = new SelectList(db.User, "usuarioId", "login", distribProcesso.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(db.User, "usuarioId", "login", distribProcesso.usuarioRecebeuId);
            return View(distribProcesso);
        }

        // POST: DistribProcesso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "distrib_ProcessoId,SetorOrigemId,SetorDestinoId,observacoes,pessoaId,usuarioEnviouId,usuarioRecebeuId,periciaId")] DistribProcesso distribProcesso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(distribProcesso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.periciaId = new SelectList(db.Pericia, "periciaId", "descricao", distribProcesso.periciaId);
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", distribProcesso.pessoaId);
            ViewBag.SetorDestinoId = new SelectList(db.Setor, "setorId", "descricao", distribProcesso.SetorDestinoId);
            ViewBag.SetorOrigemId = new SelectList(db.Setor, "setorId", "descricao", distribProcesso.SetorOrigemId);
            ViewBag.usuarioEnviouId = new SelectList(db.User, "usuarioId", "login", distribProcesso.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(db.User, "usuarioId", "login", distribProcesso.usuarioRecebeuId);
            return View(distribProcesso);
        }

        // GET: DistribProcesso/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistribProcesso distribProcesso = db.DistribProcesso.Find(id);
            if (distribProcesso == null)
            {
                return HttpNotFound();
            }
            return View(distribProcesso);
        }

        // POST: DistribProcesso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DistribProcesso distribProcesso = db.DistribProcesso.Find(id);
            db.DistribProcesso.Remove(distribProcesso);
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

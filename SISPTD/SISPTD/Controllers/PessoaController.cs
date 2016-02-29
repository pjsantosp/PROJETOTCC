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
    public class PessoaController : Controller
    {
        private dbSISPTD db = new dbSISPTD();

        // GET: Pessoa
        public ActionResult Index()
        {
            var pessoa = db.Pessoa.Include(p => p.Pessoa2);
            return View(pessoa.ToList());
        }

        // GET: Pessoa/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoa.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // GET: Pessoa/Create
        public ActionResult Create()
        {
            ViewBag.pessoaPai = new SelectList(db.Pessoa, "pessoaId", "cpf");
            return View();
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pessoaId,pessoaPai,dt_Cadastro,cpf,cns,rg,orgaoemissor,dt_Emissao,nome,dt_Nascimento,email,nome_Mae,nome_Pai,tel,cel")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                db.Pessoa.Add(pessoa);
                db.SaveChanges();
                return RedirectToAction("Create", "Endereco", new {pessoaId = pessoa.pessoaId });
            }

            ViewBag.pessoaPai = new SelectList(db.Pessoa, "pessoaId", "cpf", pessoa.pessoaPai);
            return View(pessoa);
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoa.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            ViewBag.pessoaPai = new SelectList(db.Pessoa, "pessoaId", "cpf", pessoa.pessoaPai);
            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pessoaId,pessoaPai,dt_Cadastro,cpf,cns,rg,orgaoemissor,dt_Emissao,nome,dt_Nascimento,email,nome_Mae,nome_Pai,tel,cel")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pessoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pessoaPai = new SelectList(db.Pessoa, "pessoaId", "cpf", pessoa.pessoaPai);
            return View(pessoa);
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoa.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Pessoa pessoa = db.Pessoa.Find(id);
            db.Pessoa.Remove(pessoa);
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

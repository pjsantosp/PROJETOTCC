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
    public class PessoaController : Controller
    {
        private dbSISPTD db = new dbSISPTD();

        // GET: Pessoa
        public ActionResult Index()
        {
            PessoaBO pBO = new PessoaBO();

            //var pessoa = db.Pessoa.Include(p => p.Pessoa2);
            return View(pBO.ObterPessoa());
        }

        // GET: Pessoa/Details/5
        public ActionResult Details(long? id)
        {
            PessoaBO pBO = new PessoaBO();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
             
            //Pessoa pessoa = db.Pessoa.Find(id);
            //if (pessoa == null)
            //{
            //    return HttpNotFound();
            //}
            return View(pBO.ObterPessoa(id));
        }

        // GET: Pessoa/Create
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Pessoa pessoa)
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
        public ActionResult CreateAcompanhate(long? acompanhate)
        {
            ViewBag.Paciente = db.Pessoa.Where(p => p.pessoaId == acompanhate).FirstOrDefault().nome;
            Pessoa objAcompanhate = new Pessoa();
            objAcompanhate.pessoaPai = acompanhate;
            return View(objAcompanhate);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateAcompanhate(Pessoa pesssoa)
        {
            
            if (ModelState.IsValid)
            {
                db.Pessoa.Add(pesssoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Pessoa pessoa)
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

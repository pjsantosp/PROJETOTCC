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
        private PessoaBO pBO = new PessoaBO();
        private dbSISPTD db = new dbSISPTD();
       
       
        public ActionResult Index(string p = "")
        {
            IEnumerable<Pessoa> listapessoa = db.Pessoa.Include("DistribProcesso").Where(x => x.cpf.Contains(p));
            return View(listapessoa);
        }

      
        public ActionResult Details(long? id)
        {
         
            return View(pBO.ObterPessoa(id));
        }

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
                pBO.CriarPessoa(pessoa);
                return RedirectToAction("Create", "Endereco", new { pessoaId = pessoa.pessoaId, tab = "tabEndereco" });
            }

            ViewBag.pessoaPai = new SelectList(pBO.ObterPessoa(), "pessoaId", "cpf", pessoa.pessoaPai);
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
        public ActionResult CreateAcompanhate(Pessoa pessoa)
        {
            
            if (ModelState.IsValid)
            {
                pBO.CriarPessoa(pessoa);
                TempData["Sucesso"] = "Acompanhante Cadastrado com Sucesso";
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(long? id)
        {
            Pessoa pessoa = pBO.ObterPessoa(id);
            ViewBag.pessoaPai = new SelectList(pBO.ObterPessoa(), "pessoaId", "cpf", pessoa.pessoaPai);
            return View(pessoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                pBO.AtualizarPessoa(pessoa);
                return RedirectToAction("Index");
            }
            ViewBag.pessoaPai = new SelectList(db.Pessoa, "pessoaId", "cpf", pessoa.pessoaPai);
            return View(pessoa);
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(long? id)
        {
          Pessoa pessoa =   pBO.ObterPessoa(id);
            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            pBO.Excluir(id);
            
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

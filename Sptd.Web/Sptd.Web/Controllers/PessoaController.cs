using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sptd.Web.Models;
using Sptd.Web.Repositories;


namespace Sptd.Web.Controllers
{
    public class PessoaController : Controller
    {
        private DbSPTD db = new DbSPTD();
        PessoaRepository repositoryPessoa = new PessoaRepository();

        
        public ActionResult Index()
        {

            return View(repositoryPessoa.ObterPessoa());
        }

       
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                pessoa.dt_Cadastro = DateTime.Now;
                repositoryPessoa.CriarPessoa(pessoa);
                //ViewBag.PessoaId = pessoa.pessoaId;
               // long pessoaId = pessoa.pessoaId;
               return RedirectToAction("Create", "Endereco", new {pessoaId = pessoa.pessoaId});
            }

            //ViewBag.fk_Endereco = new SelectList(db.Endereco, "enderecoId", "rua", pessoa.fk_Endereco);
            ViewBag.pessoaPai = new SelectList(db.Pessoa, "pessoaId", "cpf", pessoa.pessoaPai);
            return View();
        }
               
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pessoa pessoa = repositoryPessoa.ObterPessoa(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            //ViewBag.fk_Endereco = new SelectList(db.Endereco, "enderecoId", "rua", pessoa.fk_Endereco);
            ViewBag.pessoaPai = new SelectList(db.Pessoa, "pessoaId", "cpf", pessoa.pessoaPai);
            return View(pessoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Pessoa pessoa)
        {
            //[Bind(Include = "pessoaId,pessoaPai,fk_Endereco,dt_Cadastro,cpf,cns,rg,nome,dt_Nascimento,email,nome_Mae,nome_Pai,tel,cel")]
            if (ModelState.IsValid)
            {
                repositoryPessoa.Atualizar(pessoa);
                return RedirectToAction("Index");
            }
            //ViewBag.fk_Endereco = new SelectList(db.Endereco, "enderecoId", "rua", pessoa.fk_Endereco);
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
            Pessoa pessoa = repositoryPessoa.ObterPessoa(id);
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

            repositoryPessoa.Excluir(id);
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

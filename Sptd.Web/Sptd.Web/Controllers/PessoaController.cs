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
        PessoaRepository repository = new PessoaRepository();

        // GET: Pessoa
        public ActionResult Index()
        {

            return View(repository.ObterPessoa());
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
            ViewBag.fk_Endereco = new SelectList(db.Endereco, "enderecoId", "rua");
            ViewBag.pessoaPai = new SelectList(db.Pessoa, "pessoaId", "cpf");
            return View();
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pessoaId,pessoaPai,fk_Endereco,dt_Cadastro,cpf,cns,rg, orgaoemissor, dt_emissao,nome,dt_Nascimento,email,nome_Mae,nome_Pai,tel,cel")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                pessoa.dt_Cadastro = DateTime.Now;
                repository.CriarPessoa(pessoa);
                return RedirectToAction("Index");
            }

            ViewBag.fk_Endereco = new SelectList(db.Endereco, "enderecoId", "rua", pessoa.fk_Endereco);
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

            Pessoa pessoa = repository.ObterPessoa(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_Endereco = new SelectList(db.Endereco, "enderecoId", "rua", pessoa.fk_Endereco);
            ViewBag.pessoaPai = new SelectList(db.Pessoa, "pessoaId", "cpf", pessoa.pessoaPai);
            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pessoaId,pessoaPai,fk_Endereco,dt_Cadastro,cpf,cns,rg,nome,dt_Nascimento,email,nome_Mae,nome_Pai,tel,cel")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                repository.Atualizar(pessoa);
                return RedirectToAction("Index");
            }
            ViewBag.fk_Endereco = new SelectList(db.Endereco, "enderecoId", "rua", pessoa.fk_Endereco);
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
            Pessoa pessoa = repository.ObterPessoa(id);
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

            repository.Excluir(id);
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

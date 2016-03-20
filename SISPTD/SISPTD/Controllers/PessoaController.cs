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
        private EnderecoBO eBO = new EnderecoBO();
        private PessoaBO pBO = new PessoaBO();
       
       
        public ActionResult Index(string busca = "")
        {
  
          return View(pBO.ObterPessoa(busca));
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
                pessoa.dt_Cadastro = DateTime.Now;
                pBO.CriarPessoa(pessoa);
                return RedirectToAction("Create", "Endereco", new { pessoaId = pessoa.pessoaId });
            }

            ViewBag.pessoaPai = new SelectList(pBO.ObterPessoa(), "pessoaId", "cpf", pessoa.pessoaPai);
            return View(pessoa);
        }
        public ActionResult CreateAcompanhante(long? acompanhante)
        {
            Pessoa objAcompanhante = new Pessoa();
            objAcompanhante.pessoaPai = acompanhante;
            return View(objAcompanhante);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateAcompanhante(Pessoa pessoa)
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

      
    }
}

using SISPTD.BO;
using SISPTD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SISPTD.Controllers
{
    public class PessoaController : Controller
    {
        private PessoaBO pessoaBO = new PessoaBO(new dbSISPTD());
        private CidadeBO cidadeBO = new CidadeBO(new dbSISPTD());


        public ActionResult Index(string busca = "")
        {
            return View(pessoaBO.ObterPessoa(busca));
        }
        public ActionResult Pesquisar(string cpf)
        {
           
                var pessoa = pessoaBO.Selecionar().Where(p => p.cpf == cpf).FirstOrDefault();
                if (pessoa== null)
                {
                    return Json(new { Nome ="", Id = 0}, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Nome = pessoa.nome, Id = pessoa.pessoaId }, JsonRequestBehavior.AllowGet);
                }
           
        }

        public ActionResult Details(long? id)
        {

            return View(pessoaBO.SelecionarPorId(id.Value));
        }

        //[Authorize(Roles = "Funcionario, Gerente")]
        public ActionResult Create()
        {
            ViewBag.cidade = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Funcionario, Gerente")]
        public ActionResult Create(Pessoa pessoa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pessoa.dt_Cadastro = DateTime.Now;
                    pessoaBO.CalculoIdade(pessoa);
                    pessoaBO.Inserir(pessoa);
                    TempData["Sucesso"] = "Cadastrado Realizado com Sucesso!";
                    
                    pessoaBO.SelecionarPorId(pessoa.pessoaId);
                    return RedirectToAction("Edit", new {id = pessoa.pessoaId});
                }
            }
            catch (Exception e)
            {
                TempData["Erro"] = "Ops! " + e.Message;
            }

            ViewBag.cidade = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade");
            ViewBag.pessoaPai = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", pessoa.pessoaPai);
            return View(pessoa);
        }
        public ActionResult CreateAcompanhante(long? pessoaPai)
        {
            try
            {
                if (pessoaPai != null)
                {
                    if (ModelState.IsValid)
                    {
                        Pessoa acompanhante = new Pessoa();
                        acompanhante.pessoaPai = pessoaPai;
                        ViewBag.pessoaId = pessoaPai;
                        return View(acompanhante);
                    }
                }
            }
            catch (Exception)
            {
                TempData["Erro"] = "Ops! erro durante o Cadastro do Acompanhante!";
            }
           

            ViewBag.pessoaId = 0;

            return View();
         
            
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateAcompanhante(Pessoa pessoa, long? pessoaPai)
        {
            try
            {
                pessoa.pessoaPai = pessoaPai;
               
                if (ModelState.IsValid)
                {
                    pessoa.dt_Cadastro = DateTime.Now;
                    pessoaBO.CalculoIdade(pessoa);
                    pessoaBO.Inserir(pessoa);
                    TempData["Sucesso"] = "Acompanhante Cadastrado com Sucesso";
                    return RedirectToAction("Edit", new {id = pessoa.pessoaPai });
                }
            }
            catch (Exception ex)
            {

                TempData["Erro"] = "Erro durante o Cadastro de Acompanhante " + ex.Message;
            }

            
            return View();
        }

        // GET: Pessoa/Edit/5
        //[Authorize(Roles = "Funcionario, Gerente")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = pessoaBO.SelecionarPorId(id.Value);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        [HttpPost]
        //[Authorize(Roles = "Funcionario, Gerente")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                pessoaBO.Alterar(pessoa);
                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(long? id)
        {
            Pessoa pessoa = pessoaBO.SelecionarPorId(id.Value);
            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            pessoaBO.ExcluirPorId(id);

            return RedirectToAction("Index");
        }


    }
}

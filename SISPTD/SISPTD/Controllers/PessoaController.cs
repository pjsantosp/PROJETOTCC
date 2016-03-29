﻿using System;
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
        private EnderecoBO enderecoBO = new EnderecoBO(new dbSISPTD());
        private PessoaBO pessoaBO = new PessoaBO(new dbSISPTD());
       
       
        public ActionResult Index(string busca = "")
        {
  
          return View(pessoaBO.ObterPessoa(busca));
        }

        public ActionResult Details(long? id)
        {
         
           return View(pessoaBO.SelecionarPorId(id.Value));
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
                pessoaBO.Inserir(pessoa);
                return RedirectToAction("Create", "Endereco", new { pessoaId = pessoa.pessoaId });
            }

            ViewBag.pessoaPai = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", pessoa.pessoaPai);
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
                pessoaBO.Inserir(pessoa);
                TempData["Sucesso"] = "Acompanhante Cadastrado com Sucesso";
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
            Pessoa pessoa = pessoaBO.SelecionarPorId(id.Value);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Pessoa pessoa)
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
          Pessoa pessoa =   pessoaBO.SelecionarPorId(id.Value);
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

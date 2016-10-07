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
    public class ProcessoController : Controller
    {
        private ProcessoBO ProcessoBO = new ProcessoBO(new dbSISPTD());
        private SetorBO setorBO = new SetorBO(new dbSISPTD());
        private UserBO userBO = new UserBO(new dbSISPTD());
        private PessoaBO pessoaBO = new PessoaBO(new dbSISPTD());

        public ActionResult Index(string busca)
        {
            busca = Ultis.Util.RemoverMascara(busca);
            return View(ProcessoBO.ObterProcesso(busca));
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Processo Processo = ProcessoBO.SelecionarPorId(id.Value);
            if (Processo == null)
            {
                return HttpNotFound();
            }
            return View(Processo);
        }

        public ActionResult Create()
        {
            ViewBag.pessoaId = 0;
            //ViewBag.SetorDestinoId = new SelectList(setorBO.Selecionar(), "setorId", "descricao");
            //ViewBag.SetorOrigemId = new SelectList(setorBO.Selecionar(), "setorId", "descricao");
            //ViewBag.usuarioEnviouId = new SelectList(userBO.Selecionar(), "usuarioId", "login");
            //ViewBag.usuarioRecebeuId = new SelectList(userBO.Selecionar(), "usuarioId", "login");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(long? idPacienteDistrib, Processo Processo)

        {
            try
            {
                   Processo.pessoaId = idPacienteDistrib.Value;
                    var user = userBO.userLogado(User.Identity.Name);
                   // Processo.usuarioId = user.usuarioId;
                    if (ModelState.IsValid)
                    {
                        ProcessoBO.Inserir(Processo);
                        TempData["Sucesso"] = "Enviado com Sucesso! ";
                    }
                    return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Erro"] = "Ops! " + e.Message;
            }

            ViewBag.pessoaId = 0;
            //ViewBag.SetorDestinoId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", distribProcesso.SetorDestinoId);
            //ViewBag.SetorOrigemId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", distribProcesso.SetorOrigemId);
            //ViewBag.usuarioEnviouId = new SelectList(userBO.Selecionar(), "usuarioId", "login", distribProcesso.usuarioEnviouId, pessoaBO.Selecionar());
            ////ViewBag.usuarioRecebeuId = new SelectList(userBO.Selecionar(), "usuarioId", "login", distribProcesso.usuarioRecebeuId);
            //ViewBag.usuarioRecebeuId = 0;
            return View();
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Processo processo = ProcessoBO.SelecionarPorId(id.Value);
            if (processo == null)
            {
                return HttpNotFound();
            }

            ViewBag.Paciente = pessoaBO.SelecionarPorId(processo.pessoaId).nome;
            ViewBag.PessoaId = pessoaBO.SelecionarPorId(processo.pessoaId).pessoaId;
            //ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar().Where(p => p.tipo == 0), "pessoaId", "nome", distribProcesso.pessoaId);
            //ViewBag.SetorDestinoId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", distribProcesso.SetorDestinoId);
            //ViewBag.SetorOrigemId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", distribProcesso.SetorOrigemId);
            //ViewBag.usuarioEnviouId = new SelectList(userBO.Selecionar(), "usuarioId", "login", distribProcesso.usuarioEnviouId);
            //ViewBag.usuarioRecebeuId = new SelectList(userBO.Selecionar(), "usuarioId", "login", distribProcesso.usuarioRecebeuId);

            return View(processo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( long pessoaId, Processo processo)
        {
            //[Bind(Include = "distribProcesso.usuarioEnviouId, distribProcesso.usuarioRecebeuId,pessoaId")]

            //distribProcesso.pessoaId = pessoaId;
            var user = userBO.userLogado(User.Identity.Name);
              //  processo.usuarioId = user.usuarioId;
                if (ModelState.IsValid)
                {
                    ProcessoBO.Alterar(processo);
                TempData["Sucesso"] = "Enviado com Sucesso!!";
                    return RedirectToAction("Index");
                }
                ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", processo.pessoaId);
                //ViewBag.SetorDestinoId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", distribProcesso.SetorDestinoId);
                //ViewBag.SetorOrigemId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", distribProcesso.SetorOrigemId);
                //ViewBag.usuarioEnviouId = new SelectList(userBO.Selecionar(), "usuarioId", "login", distribProcesso.usuarioEnviouId);
                //ViewBag.usuarioRecebeuId = new SelectList(userBO.Selecionar(), "usuarioId", "login", distribProcesso.usuarioRecebeuId);
              
                return View(processo);
            
           


        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Processo processo = ProcessoBO.SelecionarPorId(id.Value);
            if (processo == null)
            {
                return HttpNotFound();
            }
            return View(processo);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ProcessoBO.ExcluirPorId(id);
            return RedirectToAction("Index");
        }

    }
}
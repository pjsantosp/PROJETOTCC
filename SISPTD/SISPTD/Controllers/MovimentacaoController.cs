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
    public class MovimentacaoController : Controller
    {
        private dbSISPTD db = new dbSISPTD();
        private MovimentacaoBO movimentacaoBO = new MovimentacaoBO(new dbSISPTD());
        private UserBO usuarioBO = new UserBO(new dbSISPTD());
        private ProcessoBO processoBO = new ProcessoBO(new dbSISPTD());

        public ActionResult Index(int? pagina, string buscar="")
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            return View(movimentacaoBO.ObterMovimentacao(buscar ,numeroPagina, tamanhoPagina));
        }

        public ActionResult ProcessoEmPericia(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            return PartialView(movimentacaoBO.ObterPericias(numeroPagina, tamanhoPagina));
        }
        public ActionResult ProcessoEmAgendamento(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            return PartialView(movimentacaoBO.ObterAgendamento(numeroPagina, tamanhoPagina));
        }

        public ActionResult DetalheDoMovProcesso(int? id)
        {
            if (id== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(movimentacaoBO.ObterDetalheDoProcesso(id.Value));
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacao movimentacao = db.Movimentacaos.Find(id);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            return View(movimentacao);
        }

        public ActionResult Create(long? id)
        {
            if (id!= null && id != 0)
            {
                Processo objProcesso = processoBO.SelecionarPorId(id.Value);
                ViewBag.processoId = objProcesso.processoId;
                ViewBag.pacienteId = objProcesso.pacienteId;
                ViewBag.processoPaciente = objProcesso.Paciente.nome;
            }
           
            ViewBag.setorEnviouId = new SelectList(db.Setor, "setorId", "descricao");
            ViewBag.setorRecebeuId = new SelectList(db.Setor, "setorId", "descricao");
            ViewBag.usuarioEnviouId = new SelectList(db.Usuario, "usuarioId", "login");
            ViewBag.usuarioRecebeuId = new SelectList(db.Usuario, "usuarioId", "login");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Movimentacao movimentacao)
        {
            try
            {
                if (movimentacao.setorEnviouId == movimentacao.setorRecebeuId)
                {
                    ModelState.AddModelError("", "Setor de Destino não deve ser o Mesmo de Origem");
                }
                var user = usuarioBO.userLogado(User.Identity.Name);
                movimentacao.usuarioEnviouId = user.usuarioId;
                movimentacao.dtEnvio = DateTime.Now;
                
                if (ModelState.IsValid)
                {
                    db.Movimentacaos.Add(movimentacao);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
           

            ViewBag.ProcessoId = new SelectList(db.Processo, "processoId", "Procedimento", movimentacao.ProcessoId);
            ViewBag.setorEnviouId = new SelectList(db.Setor, "setorId", "descricao", movimentacao.setorEnviouId);
            ViewBag.setorRecebeuId = new SelectList(db.Setor, "setorId", "descricao", movimentacao.setorRecebeuId);
            ViewBag.usuarioEnviouId = new SelectList(db.Usuario, "usuarioId", "login", movimentacao.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(db.Usuario, "usuarioId", "login", movimentacao.usuarioRecebeuId);
            return View(movimentacao);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacao movimentacao = db.Movimentacaos.Find(id);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProcessoId = new SelectList(db.Processo, "processoId", "Procedimento", movimentacao.ProcessoId);
            ViewBag.setorEnviouId = new SelectList(db.Setor, "setorId", "descricao", movimentacao.setorEnviouId);
            ViewBag.setorRecebeuId = new SelectList(db.Setor, "setorId", "descricao", movimentacao.setorRecebeuId);
            ViewBag.usuarioEnviouId = new SelectList(db.Usuario, "usuarioId", "login", movimentacao.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(db.Usuario, "usuarioId", "login", movimentacao.usuarioRecebeuId);
            return View(movimentacao);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "movimentacaoId,usuarioEnviouId,usuarioRecebeuId,setorEnviouId,setorRecebeuId,ProcessoId,dtEnvio,dtRecebimento")] Movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                movimentacao.dtEnvio = DateTime.Now;
                db.Entry(movimentacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProcessoId = new SelectList(db.Processo, "processoId", "Procedimento", movimentacao.ProcessoId);
            ViewBag.setorEnviouId = new SelectList(db.Setor, "setorId", "descricao", movimentacao.setorEnviouId);
            ViewBag.setorRecebeuId = new SelectList(db.Setor, "setorId", "descricao", movimentacao.setorRecebeuId);
            ViewBag.usuarioEnviouId = new SelectList(db.Usuario, "usuarioId", "login", movimentacao.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(db.Usuario, "usuarioId", "login", movimentacao.usuarioRecebeuId);
            return View(movimentacao);
        }

        // GET: Movimentacao/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacao movimentacao = db.Movimentacaos.Find(id);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            return View(movimentacao);
        }

        // POST: Movimentacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Movimentacao movimentacao = db.Movimentacaos.Find(id);
            db.Movimentacaos.Remove(movimentacao);
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

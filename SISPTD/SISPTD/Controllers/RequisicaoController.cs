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
    public class RequisicaoController : Controller
    {
        private dbSISPTD db = new dbSISPTD();
        private RequisicaoBO requisicaoBO = new RequisicaoBO(new dbSISPTD());
        private ItemRequisicaoBO itemRequisicaoBO = new ItemRequisicaoBO(new dbSISPTD());
        private CidadeBO cidadeBO = new CidadeBO(new dbSISPTD());

        public ActionResult Index()
        {
            return View(requisicaoBO.Selecionar());
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisicao requisicao = requisicaoBO.SelecionarPorId(id.Value);
            if (requisicao == null)
            {
                return HttpNotFound();
            }
            return View(requisicao);
        }
        public ActionResult Create()
        {
            ViewBag.IdCidadeDestino = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade");
            ViewBag.IdCidadeOrigem = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade");
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf");
            ViewBag.usuarioId = new SelectList(db.User, "usuarioId", "login");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Requisicao requisicao)
        {
            if (ModelState.IsValid)
            {
                requisicaoBO.Inserir(requisicao);
                return Json( new {Requisicao = requisicao.requisicaoId },JsonRequestBehavior.AllowGet);
            }

            ViewBag.IdCidadeDestino = new SelectList(db.Cidades, "IdCidade", "Cidade", requisicao.IdCidadeDestino);
            ViewBag.IdCidadeOrigem = new SelectList(db.Cidades, "IdCidade", "Cidade", requisicao.IdCidadeOrigem);
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", requisicao.pessoaId);
            ViewBag.usuarioId = new SelectList(db.User, "usuarioId", "login", requisicao.usuarioId);
            return View(requisicao);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisicao requisicao = db.Requisicao.Find(id);
            if (requisicao == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCidadeDestino = new SelectList(db.Cidades, "IdCidade", "Cidade", requisicao.IdCidadeDestino);
            ViewBag.IdCidadeOrigem = new SelectList(db.Cidades, "IdCidade", "Cidade", requisicao.IdCidadeOrigem);
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", requisicao.pessoaId);
            ViewBag.usuarioId = new SelectList(db.User, "usuarioId", "login", requisicao.usuarioId);
            return View(requisicao);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "requisicaoId,usuarioId,pessoaId,IdCidadeOrigem,IdCidadeDestino,observacoes,trecho")] Requisicao requisicao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requisicao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCidadeDestino = new SelectList(db.Cidades, "IdCidade", "Cidade", requisicao.IdCidadeDestino);
            ViewBag.IdCidadeOrigem = new SelectList(db.Cidades, "IdCidade", "Cidade", requisicao.IdCidadeOrigem);
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", requisicao.pessoaId);
            ViewBag.usuarioId = new SelectList(db.User, "usuarioId", "login", requisicao.usuarioId);
            return View(requisicao);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisicao requisicao = db.Requisicao.Find(id);
            if (requisicao == null)
            {
                return HttpNotFound();
            }
            return View(requisicao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Requisicao requisicao = db.Requisicao.Find(id);
            db.Requisicao.Remove(requisicao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
               
    }
}

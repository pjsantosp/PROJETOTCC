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
    public class DistribProcessoController : Controller
    {
        private DistribProcessoBO dBO = new DistribProcessoBO();
        private SetorBO sBO = new SetorBO();
        private UserBO uBO = new UserBO();
        private PessoaBO pBO = new PessoaBO();
        private dbSISPTD db = new dbSISPTD();

        // GET: DistribProcesso
        public ActionResult Index()
        {
            
            return View(dBO.ObterProcesso());

        }

        // GET: DistribProcesso/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistribProcesso distribProcesso = dBO.ObterProcesso(id);
            if (distribProcesso == null)
            {
                return HttpNotFound();
            }
            return View(distribProcesso);
        }

        // GET: DistribProcesso/Create
        public ActionResult Create()
        {
            ViewBag.pessoaId = new SelectList(dBO.ObterProcesso(), "pessoaId", "nome");
            ViewBag.SetorDestinoId = new SelectList(sBO.ObterSetor(), "setorId", "descricao");
            ViewBag.SetorOrigemId = new SelectList(sBO.ObterSetor(), "setorId", "descricao");
            //ViewBag.usuarioEnviouId = new SelectList(db.User, "usuarioId", "login");
            ViewBag.usuarioEnviouId = new SelectList(uBO.ObterUsuario(), "usuarioId", "login");
            ViewBag.usuarioRecebeuId = new SelectList(uBO.ObterUsuario(), "usuarioId", "login");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DistribProcesso distribProcesso)
        {

            var user = db.User.Where(u => u.login.Contains(User.Identity.Name)).FirstOrDefault();
            distribProcesso.UserEnviou = user;
            if (ModelState.IsValid)
            {
                db.DistribProcesso.Add(distribProcesso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pessoaId = new SelectList(pBO.ObterPessoa(), "pessoaId", "cpf", distribProcesso.pessoaId);
            ViewBag.SetorDestinoId = new SelectList(sBO.ObterSetor(), "setorId", "descricao", distribProcesso.SetorDestinoId);
            ViewBag.SetorOrigemId = new SelectList(sBO.ObterSetor(), "setorId", "descricao", distribProcesso.SetorOrigemId);
            ViewBag.usuarioEnviouId = new SelectList(uBO.ObterUsuario(), "usuarioId", "login", distribProcesso.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(uBO.ObterUsuario(), "usuarioId", "login", distribProcesso.usuarioRecebeuId);
            return View(distribProcesso);
        }

        // GET: DistribProcesso/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistribProcesso distribProcesso = dBO.ObterProcesso(id);
            if (distribProcesso == null)
            {
                return HttpNotFound();
            }
            //ViewBag.periciaId = new SelectList(db.Pericia, "periciaId", "descricao", distribProcesso.periciaId);
            ViewBag.pessoaId = new SelectList(pBO.ObterPessoa(), "pessoaId", "cpf", distribProcesso.pessoaId);
            ViewBag.SetorDestinoId = new SelectList(sBO.ObterSetor(), "setorId", "descricao", distribProcesso.SetorDestinoId);
            ViewBag.SetorOrigemId = new SelectList(sBO.ObterSetor(), "setorId", "descricao", distribProcesso.SetorOrigemId);
            ViewBag.usuarioEnviouId = new SelectList(uBO.ObterUsuario(), "usuarioId", "login", distribProcesso.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(uBO.ObterUsuario(), "usuarioId", "login", distribProcesso.usuarioRecebeuId);
            return View(distribProcesso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( DistribProcesso distribProcesso)
        {
            if (ModelState.IsValid)
            {
                dBO.AtualizarProcesso(distribProcesso);
                return RedirectToAction("Index");
            }
            ViewBag.pessoaId = new SelectList(pBO.ObterPessoa(), "pessoaId", "cpf", distribProcesso.pessoaId);
            ViewBag.SetorDestinoId = new SelectList(sBO.ObterSetor(), "setorId", "descricao", distribProcesso.SetorDestinoId);
            ViewBag.SetorOrigemId = new SelectList(sBO.ObterSetor(), "setorId", "descricao", distribProcesso.SetorOrigemId);
            ViewBag.usuarioEnviouId = new SelectList(uBO.ObterUsuario(), "usuarioId", "login", distribProcesso.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(uBO.ObterUsuario(), "usuarioId", "login", distribProcesso.usuarioRecebeuId);
            return View(distribProcesso);
        }

        // GET: DistribProcesso/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistribProcesso distribProcesso = dBO.ObterProcesso(id);
            if (distribProcesso == null)
            {
                return HttpNotFound();
            }
            return View(distribProcesso);
        }

        // POST: DistribProcesso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            dBO.Excluir(id);
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

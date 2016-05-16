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
        private DistribProcessoBO distribProcessoBO = new DistribProcessoBO(new dbSISPTD());
        private SetorBO setorBO = new SetorBO(new dbSISPTD());
        private UserBO userBO = new UserBO(new dbSISPTD());
        private PessoaBO pessoaBO = new PessoaBO(new dbSISPTD());

        public ActionResult Index(string busca)
        {
            busca = Ultis.Util.RemoverMascara(busca);
            return View(distribProcessoBO.ObterProcesso(busca));
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistribProcesso distribProcesso = distribProcessoBO.SelecionarPorId(id.Value);
            if (distribProcesso == null)
            {
                return HttpNotFound();
            }
            return View(distribProcesso);
        }

        public ActionResult Create()
        {
            ViewBag.pessoaId = 0;
            ViewBag.SetorDestinoId = new SelectList(setorBO.Selecionar(), "setorId", "descricao");
            ViewBag.SetorOrigemId = new SelectList(setorBO.Selecionar(), "setorId", "descricao");
            ViewBag.usuarioEnviouId = new SelectList(userBO.Selecionar(), "usuarioId", "login");
            ViewBag.usuarioRecebeuId = new SelectList(userBO.Selecionar(), "usuarioId", "login");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(long? idPacienteDistrib, DistribProcesso distribProcesso)
        {
            try
            {
                distribProcesso.pessoaId = idPacienteDistrib.Value ;
                var user = userBO.userLogado(User.Identity.Name);
                distribProcesso.usuarioEnviouId = user.usuarioId;
                if (ModelState.IsValid)
                {
                    distribProcessoBO.Inserir(distribProcesso);
                    TempData["Sucesso"] = "Enviado com Sucesso! ";
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Erro"] = "Ops! " + e.Message;
            }

            ViewBag.pessoaId = 0;
            ViewBag.SetorDestinoId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", distribProcesso.SetorDestinoId);
            ViewBag.SetorOrigemId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", distribProcesso.SetorOrigemId);
            ViewBag.usuarioEnviouId = new SelectList(userBO.Selecionar(), "usuarioId", "login", distribProcesso.usuarioEnviouId, pessoaBO.Selecionar());
            ViewBag.usuarioRecebeuId = new SelectList(userBO.Selecionar(), "usuarioId", "login", distribProcesso.usuarioRecebeuId);
            return View();
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistribProcesso distribProcesso = distribProcessoBO.SelecionarPorId(id.Value);
            if (distribProcesso == null)
            {
                return HttpNotFound();
            }
            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", distribProcesso.pessoaId);
            ViewBag.SetorDestinoId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", distribProcesso.SetorDestinoId);
            ViewBag.SetorOrigemId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", distribProcesso.SetorOrigemId);
            ViewBag.usuarioEnviouId = new SelectList(userBO.Selecionar(), "usuarioId", "login", distribProcesso.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(userBO.Selecionar(), "usuarioId", "login", distribProcesso.usuarioRecebeuId);
            return View(distribProcesso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DistribProcesso distribProcesso)
        {
            if (ModelState.IsValid)
            {
                distribProcessoBO.Alterar(distribProcesso);
                return RedirectToAction("Index");
            }
            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", distribProcesso.pessoaId);
            ViewBag.SetorDestinoId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", distribProcesso.SetorDestinoId);
            ViewBag.SetorOrigemId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", distribProcesso.SetorOrigemId);
            ViewBag.usuarioEnviouId = new SelectList(userBO.Selecionar(), "usuarioId", "login", distribProcesso.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(userBO.Selecionar(), "usuarioId", "login", distribProcesso.usuarioRecebeuId);
            return View(distribProcesso);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistribProcesso distribProcesso = distribProcessoBO.SelecionarPorId(id.Value);
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
            distribProcessoBO.ExcluirPorId(id);
            return RedirectToAction("Index");
        }

    }
}

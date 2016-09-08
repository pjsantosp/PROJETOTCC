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
using PagedList;

namespace SISPTD.Controllers
{
    public class AgendamentoController : Controller
    {
        private UserBO usuarioBO = new UserBO(new dbSISPTD());
        private PessoaBO pessoaBO = new PessoaBO(new dbSISPTD());
        private AgendamentoBO agendamentoBO = new AgendamentoBO(new dbSISPTD());

        public ActionResult Index(int? pagina)
        {
            int nPorPagina = 10;
            int tamPagina = pagina ?? 1;
            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar().Where(p => p.tipo == 0), "pessoaId", "cpf");
            return View(agendamentoBO.Selecionar().OrderBy(a=> a.dt_Agendamento).ToPagedList(tamPagina, nPorPagina));
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamento agendamento = agendamentoBO.SelecionarPorId(id.Value);
            if (agendamento == null)
            {
                return HttpNotFound();
            }
            return View(agendamento);
        }

        public ActionResult Create()
        {
            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar().Where(p => p.tipo == 0), "pessoaId", "cpf");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Agendamento agendamento)
        {
            try
            {
                var user = usuarioBO.userLogado(User.Identity.Name);
                agendamento.usuarioId = user.usuarioId;
                if (ModelState.IsValid)
                {
                    agendamento.dt_Marcacao = DateTime.Now;
                    agendamentoBO.Inserir(agendamento);
                    TempData["Sucesso"] = "Agendamento Realizado com Sucesso!";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["Erro"] = "Ops! " + ex.Message;
            }


            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", agendamento.pessoaId);
            //ViewBag.usuarioId = new SelectList(db.User, "usuarioId", "login", agendamento.usuarioId);
            return View(agendamento);
        }

        // GET: Agendamento/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamento agendamento = agendamentoBO.SelecionarPorId(id.Value);
            if (agendamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", agendamento.pessoaId);
            ViewBag.usuarioId = new SelectList(usuarioBO.Selecionar(), "usuarioId", "login", agendamento.usuarioId);
            return View(agendamento);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pessoaId,observacoes,usuarioId,dt_Agendamento,dt_Marcacao,clinicaId,agendamentoId")]Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                agendamentoBO.Alterar(agendamento);
                return RedirectToAction("Index");
            }
            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", agendamento.pessoaId);
            ViewBag.usuarioId = new SelectList(usuarioBO.Selecionar(), "usuarioId", "login", agendamento.usuarioId);
            return View(agendamento);
        }

        // GET: Agendamento/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamento agendamento = agendamentoBO.SelecionarPorId(id.Value);
            if (agendamento == null)
            {
                return HttpNotFound();
            }
            return View(agendamento);
        }

        // POST: Agendamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Agendamento agendamento = agendamentoBO.SelecionarPorId(id);
            agendamentoBO.Excluir(agendamento);
            return RedirectToAction("Index");
        }

    }
}

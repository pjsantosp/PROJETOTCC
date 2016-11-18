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
        private ProcessoBO processoBO = new ProcessoBO(new dbSISPTD());

        public ActionResult Index(int? pagina)
        {
            int nPorPagina = 10;
            int tamPagina = pagina ?? 1;
            return View(agendamentoBO.ObterAgendamento(tamPagina, nPorPagina));
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

        public ActionResult Create(int? processoId)
        {
            if (processoId != null)
            {
                Processo objProcesso = processoBO.SelecionarPorId(processoId.Value);
               
                ViewBag.processoId = objProcesso.processoId;
                Pessoa paciente = objProcesso.Paciente;
                ViewBag.pacienteId = processoId;
                ViewBag.pacienteNome = paciente.nome;
                ViewBag.pacienteCpf = paciente.cpf;


            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? processoId, int? pacienteId, Agendamento agendamento)
        {
            try
            {
                Processo objProcesso = agendamentoBO.ObterProcessoAgd(pacienteId.Value);
                if (agendamento.processoId == 0)
                {
                    agendamento.processoId = objProcesso.processoId;
                }
                var user = usuarioBO.userLogado(User.Identity.Name);
                agendamento.usuarioId = user.usuarioId;

                agendamento.dt_Marcacao = DateTime.Now;
                agendamentoBO.Inserir(agendamento);
               
                
               
                TempData["Sucesso"] = "Agendamento Realizado com Sucesso!";

                return RedirectToAction("Create","Movimentacao", new { id = agendamento.processoId });
            }
            catch (Exception ex)
            {

                TempData["Erro"] = "Ops! " + ex.Message;
            }


            return View(agendamento);
        }

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
            ViewBag.usuarioId = new SelectList(usuarioBO.Selecionar(), "usuarioId", "login", agendamento.usuarioId);
            return View(agendamento);
        }

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

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
    [Authorize(Roles = "Administrador, Gerente, Funcionario")]
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
                if (!agendamentoBO.VerificaAgendamento(agendamento))
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

                    return RedirectToAction("Create", "Movimentacao", new { id = agendamento.processoId });
                }
                else
                {
                    TempData["Erro"] = "Ops! Já existe um Agendamento para este Paciente nesta  Data";

                }
               
                return RedirectToAction("Index");
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
            ViewBag.processoId = agendamento.Processo.processoId;
            ViewBag.pacienteNome = agendamento.Processo.Paciente.nome;
            ViewBag.pacienteCpf = agendamento.Processo.Paciente.cpf;
            ViewBag.clinica = agendamento.Clinica.nome_Clinica;
            ViewBag.clinicaIdAtual = agendamento.clinicaId;
            return View(agendamento);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? processoId, int? clinicaIdAtual,  Agendamento agendamento)
        {
            if (agendamento.clinicaId == null)
            {
                agendamento.clinicaId = clinicaIdAtual;
            }
            var user = usuarioBO.userLogado(User.Identity.Name);
            agendamento.usuarioId = user.usuarioId;
            agendamento.dt_Marcacao = DateTime.Now;
            if (ModelState.IsValid)
            {
                agendamentoBO.Alterar(agendamento);
                TempData["Sucesso"] = "Alteração Realizada com Sucesso!";
                return RedirectToAction("Index");
            }
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

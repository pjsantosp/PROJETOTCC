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
using PagedList;
using SISPTD.Ultis;

namespace SISPTD.Controllers
{
    [Authorize(Roles = "Administrador, Gerente, Funcionario")]
    public class AgendamentoController : Controller
    {
        private UserBO usuarioBO = new UserBO(new dbSISPTD());
        private PessoaBO pessoaBO = new PessoaBO(new dbSISPTD());
        private AgendamentoBO agendamentoBO = new AgendamentoBO(new dbSISPTD());
        private ClinicaBO clinicaBO = new ClinicaBO(new dbSISPTD());

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
            var usuario = usuarioBO.userLogado(User.Identity.Name);
            ViewBag.usuarioRecebeuId = usuario.usuarioId;
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
        public ActionResult Create(int ? usuarioRecebeuId, int? processoId, int? pacienteId, Agendamento agendamento)
        {
            try
            {
                if (processoId != null || agendamento.processoId != 0 )
                {
                    if (agendamento.clinicaId == null)
                    {
                        ModelState.AddModelError("", "O Agendamento é Necessário uma Clinica!");
                    }
                    else
                    {
                        if (!agendamentoBO.VerificaAgendamento(agendamento))
                        {

                            Processo objProcesso = agendamentoBO.ObterProcessoAgd(pacienteId.Value);
                            if (objProcesso != null)
                            {
                                agendamento.processoId = objProcesso.processoId;
                            }
                            processoBO.AlteraUsuarioDoProcesso(usuarioRecebeuId.Value, processoId.Value);

                            agendamento.usuarioId = usuarioRecebeuId;

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
                }
                else
                {
                    TempData["Erro"] = "Esse Agendamento Não possui um Processo! ";
                }

                return View(agendamento);
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
            
            if (agendamento.clinicaId != null )
            {
                Clinica clinica = clinicaBO.SelecionarPorId(agendamento.clinicaId.Value);
                ViewBag.clinica = clinica.nome_Clinica;
                ViewBag.clinicaIdAtual = agendamento.clinicaId;
            }
            if (agendamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.processoId = agendamento.Processo.processoId;
            ViewBag.pacienteNome = agendamento.Processo.Paciente.nome;
            ViewBag.pacienteCpf = agendamento.Processo.Paciente.cpf;
           
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
                return RedirectToAction("Index", new { tab ="profile" });
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

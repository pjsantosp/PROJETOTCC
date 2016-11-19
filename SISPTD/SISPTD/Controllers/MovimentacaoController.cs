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
    public class MovimentacaoController : Controller
    {
        private MovimentacaoBO movimentacaoBO = new MovimentacaoBO(new dbSISPTD());
        private UserBO usuarioBO = new UserBO(new dbSISPTD());
        private ProcessoBO processoBO = new ProcessoBO(new dbSISPTD());
        private SetorBO setorBO = new SetorBO(new dbSISPTD());

        public ActionResult Index(int? pagina, string buscar="")
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            return View(movimentacaoBO.ObterMovimentacao(buscar ,numeroPagina, tamanhoPagina));
        }

 
      
        public ActionResult DetalheDoMovProcesso(int? id)
        {
            ViewBag.processoId = id.Value;
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
            Movimentacao movimentacao = movimentacaoBO.SelecionarPorId(id.Value);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            return View(movimentacao);
        }

        public ActionResult Create(long? id, long? movId)
        {
            if (id!= null && id != 0)
            {
                
                Processo objProcesso = processoBO.SelecionarPorId(id.Value);
               
                ViewBag.processoId = objProcesso.processoId;
                ViewBag.pacienteId = objProcesso.pacienteId;
                ViewBag.pacienteCpf = objProcesso.Paciente.cpf;
                ViewBag.processoPaciente = objProcesso.Paciente.nome;
                ViewBag.origemProcesso = objProcesso.Setor.descricao;
                ViewBag.movId = movId;
            }
            var usuario = usuarioBO.userLogado(User.Identity.Name);
            ViewBag.setorEnviouId = new SelectList(setorBO.Selecionar(), "setorId", "descricao");
            ViewBag.setorRecebeuId = new SelectList(setorBO.Selecionar(), "setorId", "descricao");
            ViewBag.usuarioRecebeuId = new SelectList(usuarioBO.Selecionar(), "usuarioId",  "login");
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
                Processo objProcesso = processoBO.SelecionarPorId(movimentacao.ProcessoId.Value);
                objProcesso.setorId = movimentacao.setorRecebeuId;
                processoBO.Alterar(objProcesso);
               
                    
                    movimentacao.setorAtual = setorBO.SelecionarPorId(movimentacao.setorRecebeuId.Value).descricao;
                    movimentacaoBO.Inserir(movimentacao);
                    TempData["Sucesso"] = "Movimentação feita com Sucesso !";

                    return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }

            ViewBag.ProcessoId = new SelectList(processoBO.Selecionar(), "processoId", "Procedimento", movimentacao.ProcessoId);
            ViewBag.setorEnviouId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", movimentacao.setorEnviouId);
            ViewBag.setorRecebeuId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", movimentacao.setorRecebeuId);
            ViewBag.usuarioEnviouId = new SelectList(usuarioBO.Selecionar(), "usuarioId", "login", movimentacao.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(usuarioBO.Selecionar(), "usuarioId", "login", movimentacao.usuarioRecebeuId);
        
            return View(movimentacao);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacao movimentacao = movimentacaoBO.SelecionarPorId(id.Value);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProcessoId = new SelectList(processoBO.Selecionar(), "processoId", "Procedimento", movimentacao.ProcessoId);
            ViewBag.setorEnviouId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", movimentacao.setorEnviouId);
            ViewBag.setorRecebeuId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", movimentacao.setorRecebeuId);
            ViewBag.usuarioEnviouId = new SelectList(usuarioBO.Selecionar(), "usuarioId", "login", movimentacao.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(usuarioBO.Selecionar(), "usuarioId", "login", movimentacao.usuarioRecebeuId);

          
            return View(movimentacao);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "movimentacaoId,usuarioEnviouId,usuarioRecebeuId,setorEnviouId,setorRecebeuId,ProcessoId,dtEnvio,dtRecebimento")] Movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                movimentacao.dtEnvio = DateTime.Now;
                movimentacaoBO.Alterar(movimentacao);
                return RedirectToAction("Index");
            }
            ViewBag.ProcessoId = new SelectList(processoBO.Selecionar(), "processoId", "Procedimento", movimentacao.ProcessoId);
            ViewBag.setorEnviouId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", movimentacao.setorEnviouId);
            ViewBag.setorRecebeuId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", movimentacao.setorRecebeuId);
            ViewBag.usuarioEnviouId = new SelectList(usuarioBO.Selecionar(), "usuarioId", "login", movimentacao.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(usuarioBO.Selecionar(), "usuarioId", "login", movimentacao.usuarioRecebeuId);
            return View(movimentacao);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacao movimentacao = movimentacaoBO.SelecionarPorId(id.Value);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            return View(movimentacao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            movimentacaoBO.ExcluirPorId(id);
            return RedirectToAction("Index");
        }

       
    }
}

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
    public class ProcessoController : Controller
    {
        private ProcessoBO ProcessoBO = new ProcessoBO(new dbSISPTD());
        private SetorBO setorBO = new SetorBO(new dbSISPTD());
        private UserBO userBO = new UserBO(new dbSISPTD());
        private PessoaBO pessoaBO = new PessoaBO(new dbSISPTD());

        public ActionResult BuscaProcesso(long nProcesso)
        {
            if (nProcesso > 0)
            {
                var processo = ProcessoBO.SelecionarPorId(nProcesso);

                return Json(new { pacienteCpf = processo.Paciente.cpf, pacienteNome = processo.Paciente.nome }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                TempData["Erro"] = "Verifique o valor no Campo de Busca!";
            }
            return Json(false );
        }

        public ActionResult Index(int ? pagina, string buscar= "")
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;

            buscar = Ultis.Util.RemoverMascara(buscar);


            return View(ProcessoBO.ObterProcesso(buscar, numeroPagina, tamanhoPagina));
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
        public ActionResult Create(long pacienteProcessoId, long medicoProcessoId, Processo processo)

        {
            try
            {
                processo.dtCadastro = DateTime.Now;
                processo.pacienteId = pacienteProcessoId;
                processo.medicoId = medicoProcessoId;
                
              
               
                if (ModelState.IsValid)
                {
                    ProcessoBO.Inserir(processo);
                    TempData["Sucesso"] = "Cadastro Realizado com Sucesso! ";
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

            ViewBag.Paciente = pessoaBO.SelecionarPorId(processo.pacienteId.Value).nome;
            ViewBag.PessoaId = pessoaBO.SelecionarPorId(processo.pacienteId.Value).pessoaId;
            //ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar().Where(p => p.tipo == 0), "pessoaId", "nome", distribProcesso.pessoaId);
            //ViewBag.SetorDestinoId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", distribProcesso.SetorDestinoId);
            //ViewBag.SetorOrigemId = new SelectList(setorBO.Selecionar(), "setorId", "descricao", distribProcesso.SetorOrigemId);
            //ViewBag.usuarioEnviouId = new SelectList(userBO.Selecionar(), "usuarioId", "login", distribProcesso.usuarioEnviouId);
            //ViewBag.usuarioRecebeuId = new SelectList(userBO.Selecionar(), "usuarioId", "login", distribProcesso.usuarioRecebeuId);

            return View(processo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long pessoaId, Processo processo)
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
            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", processo.pacienteId);
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

using System;
using System.Net;
using System.Web.Mvc;
using SISPTD.Models;
using SISPTD.BO;

namespace SISPTD.Controllers
{
    public class PericiaController : Controller
    {
        private ProcessoBO processoBO = new ProcessoBO(new dbSISPTD());
        private PericiaBO periciaBO = new PericiaBO(new dbSISPTD());
        private PessoaBO pessoBO = new PessoaBO(new dbSISPTD());
        private MovimentacaoBO movimentacaoBO = new MovimentacaoBO(new dbSISPTD());
        private UserBO usuarioBO = new UserBO(new dbSISPTD());



        public ActionResult Index(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;

            return View(periciaBO.ObterPericia(numeroPagina, tamanhoPagina));
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pericia pericia = periciaBO.SelecionarPorId(id.Value);
            if (pericia == null)
            {
                return HttpNotFound();
            }
            return View(pericia);
        }

        public ActionResult Create(int? processoId)
        {
            User objUsuario = usuarioBO.userLogado(User.Identity.Name);
            ViewBag.usuarioRecebuId = objUsuario.usuarioId;
            if (processoId != null)
            {
                Processo objProcesso = processoBO.SelecionarPorId(processoId.Value);
                Pessoa objPaciente = objProcesso.Paciente;
                ViewBag.processoId = objProcesso.processoId;
                ViewBag.pacienteCpf = objPaciente.cpf;
                ViewBag.pacienteNome = objPaciente.nome;
                ViewBag.pacienteId = objPaciente.pessoaId;
            }
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(int ? usuarioId, int? pacientepessoaId, Pericia pericia)
        {
            try
            {

                if (!periciaBO.VerificaPericia(pericia))
                {
                    pericia.dt_Pericia = DateTime.Now;
                    periciaBO.Inserir(pericia);
                    TempData["Sucesso"] = "Pericia Cadastrada com Sucesso";
                    return RedirectToAction("Index", new { tab = "profile" });
                }
                else
                {
                    TempData["Erro"] = "Esse Processo já foi Periciado";
                  return  RedirectToAction("Index");
                }
               
               
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Ops ! " + ex.Message;
            }
            return View(pericia);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pericia pericia = periciaBO.SelecionarPorId(id.Value);
            if (pericia == null)
            {
                return HttpNotFound();
            }
            return View(pericia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "periciaId,descricao,cidId,dt_Pericia,medicoId,situacao,pessoaId")] Pericia pericia)
        {
            if (ModelState.IsValid)
            {
                periciaBO.Alterar(pericia);
                return RedirectToAction("Index");
            }
            return View(pericia);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pericia pericia = periciaBO.SelecionarPorId(id.Value);
            if (pericia == null)
            {
                return HttpNotFound();
            }
            return View(pericia);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            periciaBO.ExcluirPorId(id);
            return RedirectToAction("Index");
        }

       
    }
}

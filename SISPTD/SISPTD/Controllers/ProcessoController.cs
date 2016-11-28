using System;
using System.Net;
using System.Web.Mvc;
using SISPTD.Models;
using SISPTD.BO;
namespace SISPTD.Controllers
{
    public class ProcessoController : Controller
    {
        private ProcessoBO processoBO = new ProcessoBO(new dbSISPTD());
        private SetorBO setorBO = new SetorBO(new dbSISPTD());
        private UserBO userBO = new UserBO(new dbSISPTD());
        private PessoaBO pessoaBO = new PessoaBO(new dbSISPTD());

        public ActionResult BuscaProcesso(long nProcesso)
        {
            if (nProcesso > 0)
            {
                var processo = processoBO.SelecionarPorId(nProcesso);

                return Json(new
                {
                    ProcessoId = processo.processoId,
                    pacienteCpf = processo.Paciente.cpf,
                    pacienteNome = processo.Paciente.nome,
                    PacienteId = processo.pacienteId,
                    origemProcesso = processo.Setor
                },
                JsonRequestBehavior.AllowGet);

            }
            else
            {
                TempData["Erro"] = "Verifique o valor no Campo de Busca!";
            }
            return Json(false);
        }
        public ActionResult ProcessoEmAgendamento(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            return PartialView(processoBO.ObterAgendamento(numeroPagina, tamanhoPagina));
        }
        public ActionResult ProcessoEmPericia(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            return PartialView(processoBO.ObterPericias(numeroPagina, tamanhoPagina));
        }

        public ActionResult Index(int? pagina, string buscar = "")
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;

            buscar = Ultis.Util.RemoverMascara(buscar);


            return View(processoBO.ObterProcesso(buscar, numeroPagina, tamanhoPagina));
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Processo Processo = processoBO.SelecionarPorId(id.Value);
            if (Processo == null)
            {
                return HttpNotFound();
            }
            return View(Processo);
        }

        public ActionResult Create()
        {
            ViewBag.pessoaId = 0;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(long? pacienteProcessoId, long? medicoProcessoId, Processo processo)

        {
            try
            {
                var usuario = userBO.userLogado(User.Identity.Name);

                if (ModelState.IsValid)
                {
                    processo.dtCadastro = DateTime.Now;
                    processo.pacienteId = pacienteProcessoId;
                    processo.medicoId = medicoProcessoId;
                    processo.Setor = "RECEPÇÃO";
                    processo.Usuario = usuario.usuarioId;
                    processoBO.Inserir(processo);
                    TempData["Sucesso"] = "Cadastro Realizado com Sucesso! ";
                    return RedirectToAction("Index");

                }



            }
            catch (Exception e)
            {
                TempData["Erro"] = "Ops! " + e.Message;
            }

            ViewBag.pessoaId = 0;

            return View(processo);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Processo processo = processoBO.SelecionarPorId(id.Value);
            if (processo == null)
            {
                return HttpNotFound();
            }
            Pessoa objPessoa = pessoaBO.SelecionarPorId(processo.pacienteId.Value);
            ViewBag.Paciente = objPessoa.nome;
            ViewBag.PessoaId = objPessoa.pessoaId;


            return View(processo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Processo processo)
        {

            try
            {
                processo.Paciente = pessoaBO.SelecionarPorId(processo.pacienteId.Value);
                processoBO.Alterar(processo);
                TempData["Sucesso"] = "Enviado com Sucesso!!";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }


            //ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", processo.pacienteId);

            //return View(processo);

        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Processo processo = processoBO.SelecionarPorId(id.Value);
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
            processoBO.ExcluirPorId(id);
            return RedirectToAction("Index");
        }

    }
}

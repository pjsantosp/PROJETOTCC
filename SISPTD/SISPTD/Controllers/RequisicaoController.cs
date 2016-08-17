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
using Rotativa;
using Rotativa.Options;

namespace SISPTD.Controllers
{
    public class RequisicaoController : Controller
    {
        private PessoaBO pessoaBO = new PessoaBO(new dbSISPTD());
        private RequisicaoBO requisicaoBO = new RequisicaoBO(new dbSISPTD());
        private UserBO usuarioBO = new UserBO(new dbSISPTD());
        private CidadeBO cidadeBO = new CidadeBO(new dbSISPTD());
        private CidBO cidBO = new CidBO(new dbSISPTD());
        private PessoaRequisicaoBO pessoaRequisicaoBO = new PessoaRequisicaoBO(new dbSISPTD());

        public ActionResult Index()
        {
            return View(requisicaoBO.ObterRequisicao());
        }

        public ActionResult AddAcompanhante(long? pacienteId, int id, List<Pessoa> Pessoa)
        {
            try
            {
                if (pacienteId != null)
                {
                    Pessoa acompanhante = pessoaBO.SelecionarPorId(id);
                    Pessoa paciente = pessoaBO.SelecionarPorId(pacienteId.Value);
                    if (paciente.pessoaId == acompanhante.pessoaPai)
                    {
                        Pessoa = Pessoa ?? new List<Pessoa>();
                        if (Pessoa.Count() <= 3)
                        {
                            Pessoa.Add(acompanhante);
                            return PartialView("_ListaPessoa", Pessoa);
                        }
                        else
                        {
                            TempData["Erro"] = "Só é permitido três Acompanhante!";
                        }

                    }
                    else
                    {
                        TempData["Erro"] = "A pessoa pesquisada não é Acomptanhante de " + paciente.nome;
                    }

                }
                else
                {
                    TempData["Erro"] = "Não existe paciente para o Acompanhante pesquisado!";
                }
            }
            catch (Exception ex)
            {

                TempData["Erro"] = "Ops! " + ex.Message;
            }

            return PartialView("_ErrosSystem");
        }
        public ActionResult RemoveAcompanhante(int id, List<Pessoa> Pessoa)
        {
            var acompanhante = Pessoa[id];
            Pessoa = Pessoa ?? new List<Pessoa>();
            Pessoa.Remove(acompanhante);
            return PartialView("_ListaPessoa", Pessoa);
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisicao requisicao = requisicaoBO.SelecionarPorId(id.Value);
            if (requisicao == null)
            {
                return HttpNotFound();
            }
            var pdf = new ViewAsPdf { Model = requisicao,PageOrientation=Orientation.Landscape };
            return pdf;


        }


        public ActionResult Create()
        {
            ViewBag.IdCidadesDestino = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade");
            ViewBag.IdCidadesOrigem = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade");
            ViewBag.usuarioId = new SelectList(usuarioBO.Selecionar(), "usuarioId", "login");
            Requisicao requisicao = new Requisicao();
            return View(requisicao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Requisicao requisicao, int? pessoaId, List<Pessoa> Pessoa)
        {


            #region Comentario
            List<Pessoa> ListaDeAcompanhante = new List<Pessoa>();
            foreach (var acompanhante in Pessoa)
            {
                ListaDeAcompanhante.Add(pessoaBO.SelecionarPorId(acompanhante.pessoaId));

            }
          


            #endregion

            var usuario = usuarioBO.userLogado(User.Identity.Name);
            requisicao.usuarioId = usuario.usuarioId;
            if (ModelState.IsValid)
            {
                requisicao.PacienteId = pessoaId.Value;
                //requisicao.PessoaAcompanhante = ListaDeAcompanhante;
                requisicao.dtRequisicao = DateTime.Now;
                //Pessoa ObjPaciente = pessoaBO.SelecionarPorId(pessoaId.Value);
                //requisicao.Paciente = ObjPaciente;
                requisicaoBO.Inserir(requisicao);

                int i = 0;
                List<PessoaRequisicao> ListaPessoaRequisicao = new List<Models.PessoaRequisicao>();
                foreach (var item in ListaDeAcompanhante)
                {
                    ListaPessoaRequisicao.Add(new PessoaRequisicao
                    {
                        pessoaId = item.pessoaId,
                        requisicaoId = requisicao.requisicaoId,
                        TipoPessoa = TipoPessoa.Acompanhante,

                    });
                    i++;
                }
                if (ListaPessoaRequisicao.Count > 0)
                {
                    pessoaRequisicaoBO.InserirLista(ListaPessoaRequisicao);
                }


            }

            ViewBag.IdCidadesDestino = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade", requisicao.IdCidadesDestino);
            ViewBag.IdCidadesOrigem = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade", requisicao.IdCidadesOrigem);
            ViewBag.usuarioId = new SelectList(usuarioBO.Selecionar(), "usuarioId", "login", requisicao.usuarioId);
            return RedirectToAction("Index");
        }
        #region Editar
        // GET: Requisicaos/Edit/5
        //public ActionResult Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Requisicao requisicao = requisicaoBO.SelecionarPorId(id.Value);
        //    if (requisicao == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.IdCidadesDestino = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade", requisicao.IdCidadesDestino);
        //    ViewBag.IdCidadesOrigem = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade", requisicao.IdCidadesOrigem);
        //    ViewBag.usuarioId = new SelectList(usuarioBO.Selecionar(), "usuarioId", "login", requisicao.usuarioId);
        //    return View(requisicao);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Requisicao requisicao)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        requisicaoBO.Alterar(requisicaoBO);
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.IdCidadesDestino = new SelectList(db.Cidades, "IdCidade", "Cidade", requisicao.IdCidadesDestino);
        //    ViewBag.IdCidadesOrigem = new SelectList(db.Cidades, "IdCidade", "Cidade", requisicao.IdCidadesOrigem);
        //    ViewBag.usuarioId = new SelectList(db.User, "usuarioId", "login", requisicao.usuarioId);
        //    return View(requisicao);
        //}
        #endregion
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisicao requisicao = requisicaoBO.SelecionarPorId(id.Value);
            if (requisicao == null)
            {
                return HttpNotFound();
            }
            return View(requisicao);
        }

    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            requisicaoBO.ExcluirPorId(id);
            return RedirectToAction("Index");
        }


    }
}

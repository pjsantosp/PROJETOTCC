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
using PagedList;

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
        private EstadoBO ufBO = new EstadoBO(new dbSISPTD());

        public ActionResult Index(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;

            return View(requisicaoBO.ObterRequisicao(numeroPagina, tamanhoPagina));
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

                        if (Pessoa.Count() < 3)
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

        public ActionResult Print(long? id)
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
            //var pdf = new ViewAsPdf { Model = requisicao, PageOrientation = Orientation.Landscape };
            return View(requisicao);


        }


        public ActionResult Create()
        {
            
            ViewBag.Estado = new SelectList(ufBO.Selecionar(), "IdEstado", "Sigla");
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

            try
            {
                if ( pessoaId == null || pessoaId == 0 )
                {
                    TempData["Erro"] = "Preencha as informações do paciente !";
                    return RedirectToAction("Create");
                }
                
                List<Pessoa> ListaDeAcompanhante = new List<Pessoa>();
                if (Pessoa != null && Pessoa.Any())
                {
                    foreach (var acompanhante in Pessoa)
                    {
                        ListaDeAcompanhante.Add(pessoaBO.SelecionarPorId(acompanhante.pessoaId));
                    }
                }

                var usuario = usuarioBO.userLogado(User.Identity.Name);
                requisicao.usuarioId = usuario.usuarioId;
                if (ModelState.IsValid)
                {
                    if (requisicao.CidadeDestino != null || requisicao.CidadeDestino == requisicao.CidadeOrigem )
                    {
                        TempData["Erro"] = "Cidade Destino não pode ser Nula ou Igual a cidade de Origem";
                        return RedirectToAction("Create");
                    }
                    requisicao.pacienteId = pessoaId.Value;
                    requisicao.dtRequisicao = DateTime.Now;
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
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }

            ViewBag.IdCidadesDestino = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade", requisicao.IdCidadesDestino);
            ViewBag.IdCidadesOrigem = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade", requisicao.IdCidadesOrigem);
            ViewBag.usuarioId = new SelectList(usuarioBO.Selecionar(), "usuarioId", "login", requisicao.usuarioId);
            return View(requisicao);
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

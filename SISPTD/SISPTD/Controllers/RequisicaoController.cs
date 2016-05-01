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
    public class RequisicaoController : Controller
    {
        private dbSISPTD db = new dbSISPTD();
        private PessoaBO pessoaBO = new PessoaBO(new dbSISPTD());
        private RequisicaoBO requisicaoBO = new RequisicaoBO(new dbSISPTD());
        // GET: Requisicaos
        public ActionResult Index()
        {
            return View(requisicaoBO.Selecionar());
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
                        Pessoa.Add(acompanhante);
                        return PartialView("_ListaPessoa", Pessoa);
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

        // GET: Requisicaos/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisicao requisicao = db.Requisicao.Find(id);
            if (requisicao == null)
            {
                return HttpNotFound();
            }
            return View(requisicao);
        }

        // GET: Requisicaos/Create
        public ActionResult Create()
        {
            //ViewBag.agendamentoId = new SelectList(db.Agendamento, "agendamentoId", "dt_Agendamento");
            ViewBag.IdCidadesDestino = new SelectList(db.Cidades, "IdCidade", "Cidade");
            ViewBag.IdCidadesOrigem = new SelectList(db.Cidades, "IdCidade", "Cidade");
            ViewBag.usuarioId = new SelectList(db.User, "usuarioId", "login");
            Requisicao requisicao = new Requisicao();
            return View(requisicao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Requisicao requisicao, int? pessoaId, List<Pessoa> pessoa)
        {
            requisicao.pacienteId = pessoaId;
            requisicao.PessoaAcompanhante = pessoa;


            if (ModelState.IsValid)
            {
                requisicao.dtRequisicao = DateTime.Now;
                db.Requisicao.Add(requisicao);
                db.SaveChanges();
            }

            return View();


            ViewBag.IdCidadesDestino = new SelectList(db.Cidades, "IdCidade", "Cidade", requisicao.IdCidadesDestino);
            ViewBag.IdCidadesOrigem = new SelectList(db.Cidades, "IdCidade", "Cidade", requisicao.IdCidadesOrigem);
            ViewBag.usuarioId = new SelectList(db.User, "usuarioId", "login", requisicao.usuarioId);
            return View(requisicao);
        }

        // GET: Requisicaos/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisicao requisicao = db.Requisicao.Find(id);
            if (requisicao == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCidadesDestino = new SelectList(db.Cidades, "IdCidade", "Cidade", requisicao.IdCidadesDestino);
            ViewBag.IdCidadesOrigem = new SelectList(db.Cidades, "IdCidade", "Cidade", requisicao.IdCidadesOrigem);
            ViewBag.usuarioId = new SelectList(db.User, "usuarioId", "login", requisicao.usuarioId);
            return View(requisicao);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "requisicaoId,usuarioId,IdCidadesOrigem,IdCidadesDestino,agendamentoId,dtRequisicao,observacoes,via,trecho")] Requisicao requisicao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requisicao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCidadesDestino = new SelectList(db.Cidades, "IdCidade", "Cidade", requisicao.IdCidadesDestino);
            ViewBag.IdCidadesOrigem = new SelectList(db.Cidades, "IdCidade", "Cidade", requisicao.IdCidadesOrigem);
            ViewBag.usuarioId = new SelectList(db.User, "usuarioId", "login", requisicao.usuarioId);
            return View(requisicao);
        }

        // GET: Requisicaos/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisicao requisicao = db.Requisicao.Find(id);
            if (requisicao == null)
            {
                return HttpNotFound();
            }
            return View(requisicao);
        }

        // POST: Requisicaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Requisicao requisicao = db.Requisicao.Find(id);
            db.Requisicao.Remove(requisicao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

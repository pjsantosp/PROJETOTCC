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
    public class PericiaController : Controller
    {
        private dbSISPTD db = new dbSISPTD();
        private ProcessoBO processoBO = new ProcessoBO(new dbSISPTD());
        private PericiaBO periciaBO = new PericiaBO(new dbSISPTD());
        private PessoaBO pessoBO = new PessoaBO(new dbSISPTD());
        private MovimentacaoBO movimentacaoBO = new MovimentacaoBO(new dbSISPTD());


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

        public ActionResult Create(int? pacienteId)
        {
            if (pacienteId != null)
            {
                Processo objProcesso = processoBO.SelecionarPorId(pacienteId.Value);
                Pessoa objPaciente = objProcesso.Paciente;
                ViewBag.processoId = objProcesso.processoId;
                ViewBag.pacienteCpf = objPaciente.cpf;
                ViewBag.pacienteNome = objPaciente.nome;
                ViewBag.pacienteId = objPaciente.pessoaId;



            }



            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? pacientepessoaId, Pericia pericia)
        {

            try
            {
                pericia.dt_Pericia = DateTime.Now;
                periciaBO.Inserir(pericia);
                
                TempData["Sucesso"] = "Pericia Cadastrada com Sucesso";

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                TempData["Erro"] = "Ops ! " + ex.Message;
            }
            //ViewBag.cidId = new SelectList(db.Cid, "cidId", "codigoCid", pericia.cidId);
            //ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", pericia.Processo.Paciente.cpf);
            return View(pericia);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pericia pericia = db.Pericia.Find(id);
            if (pericia == null)
            {
                return HttpNotFound();
            }
            ViewBag.cidId = new SelectList(db.Cid, "cidId", "codigoCid", pericia.cidId);
            //ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", pericia.Processo.Paciente.cpf);
            return View(pericia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "periciaId,descricao,cidId,dt_Pericia,medicoId,situacao,pessoaId")] Pericia pericia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pericia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cidId = new SelectList(db.Cid, "cidId", "codigoCid", pericia.cidId);
            //ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", pericia.Processo.Paciente.cpf);
            return View(pericia);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pericia pericia = db.Pericia.Find(id);
            if (pericia == null)
            {
                return HttpNotFound();
            }
            return View(pericia);
        }

        // POST: Pericia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Pericia pericia = db.Pericia.Find(id);
            db.Pericia.Remove(pericia);
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

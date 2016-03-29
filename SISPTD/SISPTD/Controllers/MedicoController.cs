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
    public class MedicoController : Controller
    {
        private MedicoBO medicoBO = new MedicoBO(new dbSISPTD());
        private PessoaBO pessoaBO = new PessoaBO(new dbSISPTD());
        private EspecialidadeBO especialidadeBO = new EspecialidadeBO(new dbSISPTD());

        // GET: Medico
        public ActionResult Index()
        {
           
            return View(medicoBO.Selecionar());
        }

        // GET: Medico/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = medicoBO.SelecionarPorId(id.Value);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // GET: Medico/Create
        public ActionResult Create()
        {
            ViewBag.especialidadeId = new SelectList(especialidadeBO.Selecionar(), "EspecialidadeId", "descricao");
            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Medico medico)
        {
            if (ModelState.IsValid)
            {
                medicoBO.Inserir(medico);
                return RedirectToAction("Index");
            }

            ViewBag.especialidadeId = new SelectList(especialidadeBO.Selecionar(), "EspecialidadeId", "descricao", medico.especialidadeId);
            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", medico.pessoaId);
            return View(medico);
        }

        // GET: Medico/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = medicoBO.SelecionarPorId(id.Value);
            if (medico == null)
            {
                return HttpNotFound();
            }
            ViewBag.especialidadeId = new SelectList(especialidadeBO.Selecionar(), "EspecialidadeId", "descricao", medico.especialidadeId);
            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", medico.pessoaId);
            return View(medico);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Medico medico)
        {
            if (ModelState.IsValid)
            {
                medicoBO.Alterar(medico);
                
                return RedirectToAction("Index");
            }
            ViewBag.especialidadeId = new SelectList(especialidadeBO.Selecionar(), "EspecialidadeId", "descricao", medico.especialidadeId);
            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", medico.pessoaId);
            return View(medico);
        }

        // GET: Medico/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico =medicoBO.SelecionarPorId(id.Value);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: Medico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            medicoBO.ExcluirPorId(id);
            return RedirectToAction("Index");
        }

     
    }
}

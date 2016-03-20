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
        private MedicoBO mBO = new MedicoBO();
        private PessoaBO pBO = new PessoaBO();
        private EspecialidadeBO eBO = new EspecialidadeBO();

        // GET: Medico
        public ActionResult Index()
        {
           
            return View(mBO.ObterMedico());
        }

        // GET: Medico/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = mBO.ObterMedico(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // GET: Medico/Create
        public ActionResult Create()
        {
            ViewBag.especialidadeId = new SelectList(eBO.ObterEspecialidade(), "EspecialidadeId", "descricao");
            ViewBag.pessoaId = new SelectList(pBO.ObterPessoa(), "pessoaId", "cpf");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Medico medico)
        {
            if (ModelState.IsValid)
            {
                mBO.CriarMedico(medico);
                return RedirectToAction("Index");
            }

            ViewBag.especialidadeId = new SelectList(eBO.ObterEspecialidade(), "EspecialidadeId", "descricao", medico.especialidadeId);
            ViewBag.pessoaId = new SelectList(pBO.ObterPessoa(), "pessoaId", "cpf", medico.pessoaId);
            return View(medico);
        }

        // GET: Medico/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = mBO.ObterMedico(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            ViewBag.especialidadeId = new SelectList(eBO.ObterEspecialidade(), "EspecialidadeId", "descricao", medico.especialidadeId);
            ViewBag.pessoaId = new SelectList(pBO.ObterPessoa(), "pessoaId", "cpf", medico.pessoaId);
            return View(medico);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Medico medico)
        {
            if (ModelState.IsValid)
            {
                mBO.AtualizarMedico(medico);
                
                return RedirectToAction("Index");
            }
            ViewBag.especialidadeId = new SelectList(eBO.ObterEspecialidade(), "EspecialidadeId", "descricao", medico.especialidadeId);
            ViewBag.pessoaId = new SelectList(pBO.ObterPessoa(), "pessoaId", "cpf", medico.pessoaId);
            return View(medico);
        }

        // GET: Medico/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico =mBO.ObterMedico(id);
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
            mBO.Excluir(id);
            return RedirectToAction("Index");
        }

     
    }
}

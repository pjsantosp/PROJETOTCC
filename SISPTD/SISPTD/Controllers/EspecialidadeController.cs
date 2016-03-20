﻿using System;
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
    public class EspecialidadeController : Controller
    {
        private EspecialidadeBO eBO = new EspecialidadeBO();

        // GET: Especialidade
        public ActionResult Index()
        {
            return View(eBO.ObterEspecialidade());
        }

        // GET: Especialidade/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = eBO.ObterEspecialidade(id);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        // GET: Especialidade/Create
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                eBO.CriarEspecialidade(especialidade);
                return RedirectToAction("Index");
            }

            return View(especialidade);
        }

        // GET: Especialidade/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = eBO.ObterEspecialidade(id);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        // POST: Especialidade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                eBO.AtualizarEspecialidade(especialidade);
                return RedirectToAction("Index");
            }
            return View(especialidade);
        }

        // GET: Especialidade/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = eBO.ObterEspecialidade(id);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        // POST: Especialidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            eBO.Excluir(id);
            return RedirectToAction("Index");
        }

       
    }
}

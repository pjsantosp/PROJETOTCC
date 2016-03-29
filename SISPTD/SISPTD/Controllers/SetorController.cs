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
    public class SetorController : Controller
    {
        private SetorBO setorBO = new SetorBO(new dbSISPTD());

        // GET: Setor
        public ActionResult Index()
        {
            return View(setorBO.Selecionar());
        }

        // GET: Setor/Details/5
        public ActionResult Details(long? id)
        {

           return View(setorBO.SelecionarPorId(id.Value));
        }

        // GET: Setor/Create
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Setor setor)
        {
            if (ModelState.IsValid)
            {
                setorBO.Inserir(setor);
                return RedirectToAction("Index");
            }

            return View(setor);
        }

        // GET: Setor/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setor setor = setorBO.SelecionarPorId(id.Value);
            if (setor == null)
            {
                return HttpNotFound();
            }
            return View(setor);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Setor setor)
        {
            if (ModelState.IsValid)
            {
                setorBO.Alterar(setor);
                return RedirectToAction("Index");
            }
            return View(setor);
        }

        // GET: Setor/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setor setor = setorBO.SelecionarPorId(id.Value);
            if (setor == null)
            {
                return HttpNotFound();
            }
            return View(setor);
        }

        // POST: Setor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            setorBO.ExcluirPorId(id);
            return RedirectToAction("Index");
        }

       
    }
}

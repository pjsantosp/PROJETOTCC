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
        private SetorBO sBO = new SetorBO();
     

        // GET: Setor
        public ActionResult Index()
        {
            return View(sBO.ObterSetor());
        }

        // GET: Setor/Details/5
        public ActionResult Details(long? id)
        {

           return View(sBO.ObterSetor(id));
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
                sBO.CriarSetor(setor);
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
            Setor setor = sBO.ObterSetor(id);
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
                sBO.AtualizarSetor(setor);
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
            Setor setor = sBO.ObterSetor(id);
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
            sBO.Excluir(id);
            return RedirectToAction("Index");
        }

       
    }
}

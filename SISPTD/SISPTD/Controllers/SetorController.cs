using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SISPTD.Models;

namespace SISPTD.Controllers
{
    public class SetorController : Controller
    {
        private dbSISPTD db = new dbSISPTD();

        // GET: Setor
        public ActionResult Index()
        {
            return View(db.Setor.ToList());
        }

        // GET: Setor/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setor setor = db.Setor.Find(id);
            if (setor == null)
            {
                return HttpNotFound();
            }
            return View(setor);
        }

        // GET: Setor/Create
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "setorId,descricao")] Setor setor)
        {
            if (ModelState.IsValid)
            {
                db.Setor.Add(setor);
                db.SaveChanges();
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
            Setor setor = db.Setor.Find(id);
            if (setor == null)
            {
                return HttpNotFound();
            }
            return View(setor);
        }

        // POST: Setor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "setorId,descricao")] Setor setor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(setor).State = EntityState.Modified;
                db.SaveChanges();
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
            Setor setor = db.Setor.Find(id);
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
            Setor setor = db.Setor.Find(id);
            db.Setor.Remove(setor);
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

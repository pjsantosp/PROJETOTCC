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
    public class UserController : Controller
    {
        private dbSISPTD db = new dbSISPTD();
        private UserBO uBO = new UserBO();

        // GET: User
        public ActionResult Index()
        {
            var user = db.User.Include(u => u.Pessoa).Take(10);
            return View(user.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Create(string user)
        {
            ViewBag.roles = new SelectList(Enum.GetValues(typeof(Tipo)));
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user, string roles)
        {
            if (uBO.VerificaUser(user) != false)
            {
                user.tipo = (Tipo)Enum.Parse(typeof(Tipo), roles);

                user.senha = uBO.Encrypt(user.senha);

                if (ModelState.IsValid)
                {
                    db.User.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            TempData["Erro"] = "O Login " + user.login + " Já Esta cadastrado para Uma Pessoa!";
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "nome", user.pessoaId);
            ViewBag.roles = new SelectList(Enum.GetValues(typeof(Tipo)));
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", user.pessoaId);
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "usuarioId,login,senha,pessoaId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", user.pessoaId);
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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

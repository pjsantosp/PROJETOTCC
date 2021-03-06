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
    public class UsuarioController : Controller
    {
        private UsuarioBO userBO = new UsuarioBO(new dbSISPTD());
        private PessoaBO pessoaBO = new PessoaBO(new dbSISPTD());

        // GET: User
        public ActionResult Index()
        {
            return View(userBO.Selecionar());
        }

        // GET: User/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario user = userBO.SelecionarPorId(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Create(string user)
        {
            ViewBag.roles = new SelectList(Enum.GetValues(typeof(Perfil)));
            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario user, string roles)
        {
            try
            {
                if (userBO.VerificaUser(user) != false)
                {
                    user.Perfil = (Perfil)Enum.Parse(typeof(Perfil), roles);

                    user.senha = userBO.Encrypt(user.senha);

                    if (ModelState.IsValid)
                    {
                        userBO.Inserir(user);
                        return RedirectToAction("Index");
                    }
                }

                TempData["Erro"] = "O Login " + user.login + " Já Esta cadastrado para Uma Pessoa!";
                ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "nome", user.pessoaId);
                ViewBag.roles = new SelectList(Enum.GetValues(typeof(Perfil)));
                return View(user);
            }
            catch (Exception)
            {

                TempData["Erro"] = "Ops! Houve um erro";
                return View();
            }
           
        }

        // GET: User/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario user = userBO.SelecionarPorId(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", user.pessoaId);
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario user)
        {
            if (ModelState.IsValid)
            {
                userBO.Alterar(user);
                return RedirectToAction("Index");
            }
            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", user.pessoaId);
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario user = userBO.SelecionarPorId(id.Value);
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
            userBO.ExcluirPorId(id);
            return RedirectToAction("Index");
        }

    }
}

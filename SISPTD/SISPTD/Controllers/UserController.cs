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
        private UserBO userBO = new UserBO(new dbSISPTD());
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
            User user = userBO.SelecionarPorId(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Create()
        {
            ViewBag.roles = new SelectList(Enum.GetValues(typeof(Perfil)));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (!Ultis.Util.ValidarCPF(user.login))
            {
                TempData["Erro"] = "O CPF informado não é valido!";
            }
            try
            {
                user.login = Ultis.Util.RemoverMascara(user.login);
                user.senha = user.login;

                if (userBO.VerificaUser(user) != false)
                {
                   // user.Perfil = (Perfil)Enum.Parse(typeof(Perfil), roles);

                    user.senha = Ultis.Util.Encrypt(user.senha);

                    if (ModelState.IsValid)
                    {
                        userBO.Inserir(user);
                        TempData["Erro"] = "Usuário Cadastrado com Sucesso";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["Erro"] = "O Login " + user.login + " Já Esta cadastrado para Uma Pessoa!";
                }


                //ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "nome", user.pessoaId);
                ViewBag.roles = new SelectList(Enum.GetValues(typeof(Perfil)));
                return View(user);
            }
            catch (Exception)
            {

                TempData["Erro"] = "Ops! Houve um erro";
                return View();
            }

        }

        public ActionResult TrocaSenha()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TrocaSenha(string login, string senha, string novaSenha, string confSenha)
        {
            

            try
            {
                login = Ultis.Util.RemoverMascara(login);
                if (!string.IsNullOrEmpty(login) && Ultis.Util.ValidarCPF(login))
                {
                    if (novaSenha != confSenha || string.IsNullOrEmpty(novaSenha))
                    {
                        TempData["Erro"] = "A nova Senha é diferente da confirmação!";
                    }
                    else
                    {
                        var usuario = userBO.Selecionar().Where(u => u.login.Contains(login)).FirstOrDefault();
                        if (usuario.senha == Ultis.Util.Encrypt(senha))
                        {
                            usuario = userBO.Validar(usuario);
                            usuario.senha = Ultis.Util.Encrypt(novaSenha);
                            userBO.Alterar(usuario);
                            TempData["Sucesso"] = "Senha alterada com Sucesso!";
                            return View();
                        }
                        else
                        {
                            TempData["Erro"] = "Senha atual não confere!";
                        }
                    }
                }
                else
                {
                    TempData["Erro"] = "Campo login em branco ou é nulo";
                }
            }
            catch (Exception ex)
            {

                TempData["Erro"] = "Não foi Possivel trocar a Senha " + ex.Message;
            }

            return View();

        }



        // GET: User/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userBO.SelecionarPorId(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.pessoaId = pessoaBO.SelecionarPorId(user.pessoaId);
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
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
            User user = userBO.SelecionarPorId(id.Value);
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

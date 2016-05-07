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
            //ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "nome");
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
                    //user.Perfil = (Perfil)Enum.Parse(typeof(Perfil), roles);

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
        public ActionResult TrocaSenha(string login, string senha, string novaSenha, string confSenha)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(login))
                {
                    TempData["Erro"] = "Login não é valido";

                }
                if (!string.IsNullOrWhiteSpace(senha))
                {
                    senha = Ultis.Util.Encrypt(senha);
                    TempData["Erro"] = "O campo senha está em branco";
                }

                if (novaSenha != confSenha)
                {
                    TempData["Erro"] = "Nova senha e a confirmação devem ser iguais !";

                }

                User usuario = new User();
                usuario.login = login;
                usuario.senha = senha;



                if (!userBO.VerificaUser(usuario))
                {
                    usuario.senha = Ultis.Util.Encrypt(novaSenha);
                    usuario.usuarioId = userBO.Selecionar().Where(u => u.login.Contains(login)).First().usuarioId;
                    usuario.senha = novaSenha;

                    userBO.Alterar(usuario);
                    TempData["Erro"] = "Senha alterada com sucesso!";
                }

                return View();
            }
            catch (Exception e)
            {

                TempData["Erro"] = "Ops! Erro durante a troca de senha" + e.Message;
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
            ViewBag.pessoaId = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", user.pessoaId);
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

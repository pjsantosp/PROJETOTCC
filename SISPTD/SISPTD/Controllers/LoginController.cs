using SISPTD.BO;
using SISPTD.Models;
using SISPTD.Ultis;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SISPTD.Controllers
{
    public class LoginController : Controller
    {
        private UserBO userBO = new UserBO(new dbSISPTD());
        private PessoaBO pessoaBO = new PessoaBO(new dbSISPTD());

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(User conta)
        {
            conta.login = Util.RemoverMascara(conta.login);
            
            try
            {
                if (String.IsNullOrWhiteSpace(conta.senha))
                {
                    ModelState.AddModelError("","Verifique o Campo Senha!");
                }
                if (conta.login == conta.senha)
                {
                    TempData["Erro"] = "Deve trocar sua senha!";
                }

                if (ModelState.IsValid)
                {
                    conta.senha = Util.Encrypt(conta.senha);
                    conta = userBO.Validar(conta);
                    if (conta != null)
                    {
                        var nomeLogin = pessoaBO.Selecionar().Where(p => p.cpf == conta.login).FirstOrDefault();
                        Session["NomeLogin"] = nomeLogin.nome;
                        if (conta != null)
                        {
                            FormsAuthentication.SetAuthCookie(conta.login, false);
                            CreateAuthorizeTicket((int)conta.usuarioId, conta.login.ToString(), conta.Perfil.ToString());
                            switch (conta.Perfil)
                            {
                                case Perfil.Gerente:
                                    return RedirectToAction("Index", "Pessoa");
                                case Perfil.Funcionario:
                                    if (conta.Setor.descricao == "AGENDAMENTO")
                                    {
                                        return RedirectToAction("Index", "Agendamento");

                                    }
                                    else if (conta.Setor.descricao == "PASSAGEM")
                                    {
                                        return RedirectToAction("Index", "Requisicao");

                                    }
                                    return RedirectToAction("Index", "Pessoa");
                                case Perfil.Medico:
                                    return RedirectToAction("Index", "Pericia");
                                case Perfil.Administrador:
                                    return RedirectToAction("Index", "User");
                                default:
                                    break;
                            }
                            return RedirectToAction("Index", "Pessoa");


                        }
                    }
                    else
                    {
                        TempData["Erro"] = "Login e/ou Senha Inválidos";
                    }
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["Erro"] = "Ops! Houve um erro " + e.Message;

            }
            return View();
        }

        public ActionResult Deslogar()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }

        private void CreateAuthorizeTicket(int id, string login, string roles)
        {

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(id, login,
              DateTime.Now,
              DateTime.Now.AddMinutes(30),
              false,
              roles);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));

            Response.Cookies.Add(cookie);

        }



    }
}
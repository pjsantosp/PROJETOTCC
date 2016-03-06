using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SISPTD.Models;
using SISPTD.BO;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Web.Security;

namespace SISPTD.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(User conta)
        {
            
            
            UserBO uBO = new UserBO();
            conta.senha = Encrypt(conta.senha);
            conta = uBO.Validar(conta); 
            if (conta != null)
            {
                FormsAuthentication.SetAuthCookie(conta.login, false);
                CreateAuthorizeTicket((int)conta.usuarioId,conta.login.ToString(),conta.tipo.ToString());

                //String returnUrl = Request.QueryString["ReturnUrl"];
                //if (returnUrl != null)
                //    return Redirect(returnUrl);
                //else
                return RedirectToAction("Index", "Pessoa");

                //Deslogar 

              //  FormsAuthentication.SignOut();
                // ususario que esta logado saber
                //if (Request.IsAuthenticated)
                //{
                    
                //}

                //regra do usuario


            }
            ViewBag.msg = "Login e/ou Senha Inválidos";
            return View();
        }
        public void Deslogar()
        {
            FormsAuthentication.SignOut();
        }

        public static string Encrypt(string senha)
        {
            var clearBytes = Encoding.UTF8.GetBytes(senha);
            using (var provider = new RijndaelManaged())
            {
                byte[] key = new byte[provider.KeySize / 8];
                byte[] initializationVector = new byte[provider.BlockSize / 8];
                ICryptoTransform transformer = provider.CreateEncryptor(key, initializationVector);
                using (var buffer = new MemoryStream())
                {
                    using (var stream = new CryptoStream(
                        stream: buffer,
                        transform: transformer,
                        mode: CryptoStreamMode.Write)
                        )
                    {
                        stream.Write(clearBytes, 0, clearBytes.Length);
                        stream.FlushFinalBlock();
                        return Convert.ToBase64String(buffer.ToArray());
                    }
                }
            }
        }

        private void CreateAuthorizeTicket(int id, string login, string roles)
        {

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(id, login,  // Id do usuário é muito importante
              DateTime.Now,
              DateTime.Now.AddMinutes(30),  // validade 30 min tá bom demais
              false,   // Se você deixar true, o cookie ficará no PC do usuário
              roles);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));

            Response.Cookies.Add(cookie);

        }

      
    }
}
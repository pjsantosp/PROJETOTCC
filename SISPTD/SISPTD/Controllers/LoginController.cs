using SISPTD.BO;
using SISPTD.Models;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SISPTD.Controllers
{
    public class LoginController : Controller
    {
        dbSISPTD db = new dbSISPTD();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(User conta)
        {
            try
            {
                UserBO userBO = new UserBO(new dbSISPTD());
                conta.senha = Encrypt(conta.senha);
                conta = userBO.Validar(conta);

                if (conta != null)
                {
                    FormsAuthentication.SetAuthCookie(conta.login, false);
                    CreateAuthorizeTicket((int)conta.usuarioId, conta.login.ToString(), conta.Perfil.ToString());

                    return RedirectToAction("Index", "Pessoa");

                }
               TempData["Erro"] = "Login e/ou Senha Inválidos";
                return View();
            }
            catch (Exception e)
            {

                TempData["Erro"] = "Ops! Houve um erro " + e.Message;
                return View();
            }


        }
        public ActionResult Deslogar()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Login");
        }

        public static string Encrypt(string senha)
        {
            try
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
            catch (Exception e)
            {
                throw new Exception("Erro de Encrypt " + e.Message);

            }

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
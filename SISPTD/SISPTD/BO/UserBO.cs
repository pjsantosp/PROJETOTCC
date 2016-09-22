using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using PagedList;

namespace SISPTD.BO
{
    public class UserBO:CrudComumEntity<User, long>
    {
        public UserBO(dbSISPTD contexto)
            :base(contexto)
        {

        }
       
        public User Validar(User conta)
        {
            try
            {
                string login = conta.login;
                string senha = conta.senha;
                var _conta = Selecionar().Where(u => u.login == login && u.senha == senha);
                if (_conta.Count() >0)
                {
                    conta = _conta.FirstOrDefault();
                    return conta;
                }
                else
                {
                    return null;
                }
 
            }
            catch (Exception ex)
            {
                
                throw new Exception( ex.Message);
            }
        }
       

        public bool VerificaUser(User user)
        {
            try
            {
                var userExist = Selecionar().FirstOrDefault(u => u.login == user.login);
                if (userExist == null)
                    return true;
                else
                    return false;
                
            }
            catch (Exception)
            {
                
                throw new Exception("Ops! erro durante a verificação do usuário");
            }
        }
        public User userLogado(string userLogado)
        {
            try
            {
                return _contexto.Set<User>().Where(u => u.login.Contains(userLogado)).SingleOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<User> ObterUsuario(int? pagina, int numPagina)
        {
            try
            {


                IEnumerable<User> listarequisicao = _contexto.Set<User>().ToList();

                return listarequisicao.OrderByDescending(s => s.Pessoa.dt_Cadastro).ToPagedList(pagina.Value, numPagina);
            }
            catch (Exception e)
            {

                throw new Exception("Erro na busca na lista de Usuários", e);
            }

        }

    }
}
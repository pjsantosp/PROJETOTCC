﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace SISPTD.BO
{
    public class UsuarioBO:CrudComumEntity<Usuario, long>
    {
        public UsuarioBO(dbSISPTD contexto)
            :base(contexto)
        {

        }
       
        public Usuario Validar(Usuario conta)
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
            catch (Exception)
            {
                
                throw new Exception("Ops! Alguma coisa deu Errado");
            }
        }
        public  string Encrypt(string senha)
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

        public bool VerificaUser(Usuario user)
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
        public Usuario userLogado(string userLogado)
        {
            try
            {
                return _contexto.Set<Usuario>().Where(u => u.login.Contains(userLogado)).SingleOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
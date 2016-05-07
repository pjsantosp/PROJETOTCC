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

namespace SISPTD.Ultis
{
    public class Util
    {

        public static string RemoverMascara(string valor)
        {
            String[] Remover = { "-", ".", "_", " ", "/" };
            return RemoverMascara(valor, Remover);
        }

        public static string RemoverMascara(string valor, String[] Remover)
        {
            if (valor == null)
            {
                return "";
            }

            foreach (string ch in Remover)
            {
                valor = valor.Replace(ch, "");
            }

            return valor;
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
        public static bool ValidarCPF(string cpf)
        {
            if (String.IsNullOrWhiteSpace(cpf))
            {
                return false;
            }

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

    }

     
        
     
}
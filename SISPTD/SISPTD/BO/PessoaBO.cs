using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;
using System.Data.Entity;

namespace SISPTD.BO
{
    public class PessoaBO : CrudComumEntity<Pessoa, long>
    {
        public PessoaBO(dbSISPTD contexto)
            : base(contexto)
        {

        }
        public override void Inserir(Pessoa entidade)
        {
            if (!ValidarCPF(entidade.cpf))
                throw new Exception("CPF Informado não é valido!");
            if (ExistPessoa(entidade))
                throw new Exception("já existe esse CPF cadastrado para uma Pessoa !");
            //if (CalculoIdade(entidade))
            //    throw new Exception("É Necessário Acompanhante Para o Paciente!");
            base.Inserir(entidade);
        }
        public IEnumerable<Pessoa> ObterPessoa(string busca)
        {
            try
            {
                IEnumerable<Pessoa> listapessoa = _contexto.Set<Pessoa>()
               .Include(d => d.DistribProcesso)
               .Where(x => x.cpf.Contains(busca));
                return listapessoa;
            }
            catch (Exception e)
            {

                throw new Exception("Erro na busca na lista de pessoa", e);
            }

        }

        public bool ValidarCPF(string cpf)
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
        public bool ExistPessoa(Pessoa pessoa)
        {
            try
            {
                var Exist = _contexto.Set<Pessoa>().FirstOrDefault(p => p.cpf == pessoa.cpf);
                if (Exist != null)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw new Exception(" Outra coisa Qualquer !");
            }
        }
        public bool CalculoIdade(Pessoa pessoa)
        {
            try
            {
                int anoNascimento = pessoa.dt_Nascimento.Year;
                int anoAtual = DateTime.Now.Year;
                int idade = anoAtual - anoNascimento;
                pessoa.idade = idade;
                if (idade < 18 || idade > 60)
                    return true;
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {

                throw new Exception("Erro durante a Verificação da Idade do Paciente", e);
            }
        }
        public string ObterPessoaLogin(User login)
        {
            try
            {
                var  loginNome = Selecionar().FirstOrDefault(p => p.cpf == login.login).ToString();
                return loginNome;
            }
            catch (Exception)
            {

                throw;
            }

           
        }

    }
}
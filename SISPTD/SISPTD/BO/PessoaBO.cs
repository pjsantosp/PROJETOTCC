using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;
using System.Data.Entity;
using PagedList.Mvc;
using PagedList;

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
            if (!Ultis.Util.ValidarCPF(entidade.cpf))
                throw new Exception("CPF Informado não é valido!");
            if (ExistPessoa(entidade))
                throw new Exception("já existe esse CPF cadastrado para uma Pessoa !");
            //if (CalculoIdade(entidade))
            //    throw new Exception("É Necessário Acompanhante Para o Paciente!");
            base.Inserir(entidade);
        }
        public IEnumerable<Pessoa> ObterPessoa(string busca, int? pagina, int tamanhoPagina)
        {
            try
            {
                IEnumerable<Pessoa> listapessoa = _contexto.Set<Pessoa>()
               .Include(d => d.ListaDeProcessosPaciente)
               .Where(x => x.cpf.Contains(busca)).Where(x => x.tipo == 0);
                return listapessoa.OrderByDescending(p => p.dt_Cadastro).ToPagedList(pagina.Value, tamanhoPagina);
            }
            catch (Exception e)
            {

                throw new Exception("Erro na busca na lista de pessoa", e);
            }

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
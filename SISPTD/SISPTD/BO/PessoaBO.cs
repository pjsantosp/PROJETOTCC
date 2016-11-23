using System;
using System.Collections.Generic;
using System.Linq;
using SISPTD.Models;
using System.Data.Entity;
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
            base.Inserir(entidade);
        }
        public IEnumerable<Pessoa> ObterPessoa(string busca, int? pagina, int tamanhoPagina)
        {
            try
            {
                IEnumerable<Pessoa> listapessoa = _contexto.Set<Pessoa>()
               .Include(d => d.ListaDeProcessosPaciente)
               .Where(x => x.cpf.Contains(busca) || x.cns.Contains(busca)).Where(x => x.TipoPessoa == TipoPessoa.Paciente);
                return listapessoa.OrderByDescending(p => p.dt_Cadastro).ToPagedList(pagina.Value, tamanhoPagina);
            }
            catch (Exception e)
            {
                throw new Exception("Erro na busca na lista de pessoa", e);
            }
        }

        public IEnumerable<Pessoa> ObterMedico(string busca, int? pagina, int tamanhoPagina)
        {
            try
            {
                IEnumerable<Pessoa> listapessoa = _contexto.Set<Pessoa>()
               .Include(d => d.ListaDeProcessosPaciente)
               .Where(x => x.cpf.Contains(busca) && x.TipoPessoa == TipoPessoa.Medico);
                return listapessoa.OrderByDescending(p => p.dt_Cadastro).ToPagedList(pagina.Value, tamanhoPagina);
            }
            catch (Exception e)
            {

                throw new Exception("Erro na busca na lista de Medico", e);
            }

        }
        public IEnumerable<Pessoa> ObterFuncionarios(string busca, int? pagina, int tamanhoPagina)
        {
            try
            {
                IEnumerable<Pessoa> listapessoa = _contexto.Set<Pessoa>()
               .Include(d => d.ListaDeProcessosPaciente)
               .Where(x => x.cpf.Contains(busca) && x.TipoPessoa == TipoPessoa.Funcionario);
                return listapessoa.OrderByDescending(p => p.dt_Cadastro).ToPagedList(pagina.Value, tamanhoPagina);
            }
            catch (Exception e)
            {
                throw new Exception("Erro na busca na lista de Funcionario", e);
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
                throw new Exception("Erro durante a Verificação da Idade de Pessoa", e);
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
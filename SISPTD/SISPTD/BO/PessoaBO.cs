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
            if (entidade.cpf != null && entidade.cpf != string.Empty)
            {
                if (!Ultis.Util.ValidarCPF(entidade.cpf))
                    throw new Exception("CPF Informado não é valido!");
                if (ExistPessoa(entidade))
                    throw new Exception("já existe esse CPF cadastrado para uma Pessoa !");
                base.Inserir(entidade);
            }
            else if (entidade.cpf == string.Empty || entidade.idade < 18)
            {
                base.Inserir(entidade);
            }
           


        }
        /// <summary>
        /// Metodo Utilizado para Obter uma lista de Pessoas do Banco 
        /// </summary>
        /// <param name="busca"></param>
        /// <param name="pagina"></param>
        /// <param name="tamanhoPagina"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  Método Responsavel por listar os medicos cadastrado
        /// </summary>
        /// <param name="busca">onde é passado o cpf do Médico para Ser Realizado a
        /// Busca</param>
        /// <param name="pagina">Variável responsável por indicar o numero da pagina
        /// atual</param>
        /// <param name="tamanhoPagina">Numero de paginas por vez</param>
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
        /// <summary>
        /// Verifica a existencia de pessoa no banco
        /// </summary>
        /// <param name="pessoa">é passado um obj do tipo pessoa</param>
        /// <returns>
        /// true ou false
        /// </returns>
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
        /// <summary>
        /// Metodo responsável por calcular a idade
        /// </summary>
        /// <param name="pessoa"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [System.ComponentModel.Description("Metodo responsável por calcular a idade")]
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
        /// <summary>
        /// Método responsavel por obter pessoa logada
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string ObterPessoaLogin(User login)
        {
            try
            {
                var loginNome = Selecionar().FirstOrDefault(p => p.cpf == login.login).ToString();
                return loginNome;
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
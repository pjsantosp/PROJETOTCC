using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class PessoaBO
    {
        private dbSISPTD db = new dbSISPTD();
        /// <summary>
        /// Metodo responsavel por Listar as 10 Primeiras pessoas cadastradas no banco
        /// </summary>
        /// <returns> Lista de Pessoas</returns>
        public List<Pessoa> ObterPessoa()
        {
             dbSISPTD db = new dbSISPTD();
            try
            {
                return db.Pessoa.Include("Pessoa2").Take(10).ToList();
            }
            catch (Exception e)
            {
              
                throw new Exception("Erro duran a listagem de Pessoa",e);
            }
        }
        /// <summary>
        /// Metodo Responsável por Listar Pessoa Passada como parametro id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Pessoa</returns>
        public Pessoa ObterPessoa(long? id)
        {
            try
            {
                dbSISPTD db = new dbSISPTD();
                Pessoa pessoa = db.Pessoa.Find(id);
                return pessoa;
            }
            catch (Exception e)
            {
                
                throw new Exception("Erro durante a busca", e);
            }
        }
       
        public Pessoa AtualizarPessoa(Pessoa pessoa)
        {

            try
            {
                db.Entry(pessoa).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return pessoa;
            }
            catch (Exception e)
            {
                
                throw new Exception("Erro durante a Atualização da Pessoa!",e);
            }
        }
        public void Excluir(long id)
        {
            Pessoa pessoa = db.Pessoa.Find(id);
            db.Pessoa.Remove(pessoa);
            db.SaveChanges();
        }
        public Pessoa CriarPessoa( Pessoa pessoa)
        {
            try
            {
                db.Pessoa.Add(pessoa);
                db.SaveChanges();
                return pessoa;
            }
            catch (Exception e)
            {
                
                throw new Exception("Não Foi Possivél Criar a Pessoa", e);
            }
        }
        public bool CalculoIdade(Pessoa pessoa)
        {
            try
            {
                int anoNascimento = pessoa.dt_Nascimento.Year;
                int anoAtual = DateTime.Now.Year;
                int idade = anoAtual - anoNascimento;

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
    }
}
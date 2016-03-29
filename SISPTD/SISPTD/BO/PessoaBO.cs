using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;
using System.Data.Entity;

namespace SISPTD.BO
{
    public class PessoaBO : CrudComumEntity<Pessoa,long>
    {
        public PessoaBO(dbSISPTD contexto)
            : base(contexto)
        {
           
        }
        public IEnumerable<Pessoa> ObterPessoa(string busca)
        {
            try
            {
                IEnumerable<Pessoa> listapessoa = _contexto.Set<Pessoa>()
               .Include(d=> d.DistribProcesso)
               .Where(x => x.cpf.Contains(busca));
                return listapessoa;
            }
            catch (Exception e)
            {
                
                throw new Exception("Erro na busca na lista de pessoa",e);
            }
            
        }
       
      public override void Inserir(Pessoa pessoa)
      {
          try
          {
             
              Inserir(pessoa);
          }
          catch (Exception)
          {
              
              throw;
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
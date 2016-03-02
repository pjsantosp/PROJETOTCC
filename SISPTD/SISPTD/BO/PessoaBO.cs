using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class PessoaBO
    {
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

    }
}
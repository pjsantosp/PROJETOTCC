using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;

namespace SISPTD.BO
{
    public class EnderecoBO
    {
        private dbSISPTD db = new dbSISPTD();
        public List<Endereco> ObterEndereco()
        {
            try
            {
                return db.Endereco.Include("Cidades").Include("Pessoa").Take(10).ToList();
            }
            catch (Exception e)
            {
                
                throw new Exception("Erro durante a listagem dos Endereço",e);
            }
        }
        public Endereco ObterEndereco(long? id)
        {
            try
            {
                Endereco endereco = db.Endereco.Find(id);
                return endereco;
            }
            catch (Exception e)
            {
                
                throw new Exception("Erro no Detalhe do Endereço", e);
            }
        }
        //public Endereco CriarEndPessoa(Endereco endereco)
        //{
        //    try
        //    {

                
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //}

    }
}
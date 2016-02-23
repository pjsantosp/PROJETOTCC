using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sptd.Web.Models.Annotations;
using Sptd.Web.Models;

namespace Sptd.Web.Repositories
{
    public class PessoaRepository
    {
        public List<Pessoa> ObterPessoa()
        {
            using ( DbSPTD db = new DbSPTD())
            {
                return db.Pessoa.Include("Endereco").Include("Pessoa2").OrderBy(p => p.nome). Take(10).ToList();
            }
        }
        public List<Pessoa> ObterPessoa(string cpf, string nome)
        {
            using (DbSPTD db = new DbSPTD())
            {
                var pessoa = db.Pessoa.Include("TipoPessoa").Include("Endereco").Where(p => p.cpf.Contains(cpf) || p.nome.Contains(nome));
                return pessoa.ToList();
            }
        }
        public Pessoa ObterPessoa(long? id)
        {
            using (DbSPTD db = new DbSPTD())
            {
               Pessoa pessoa = db.Pessoa.Find(id);
               return pessoa;
            }
        }
        public Pessoa CriarPessoa(Pessoa pessoa)
        {
            using (DbSPTD db = new DbSPTD())
            {
                
                db.Pessoa.Add(pessoa);
                db.SaveChanges();
                return pessoa;
            }
        }
        public Pessoa Atualizar(Pessoa pessoa)
        {
            using (DbSPTD db = new DbSPTD())
            {
                db.Entry(pessoa).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return pessoa;
            }
        }
        public void Excluir(long id)
        {
            using (DbSPTD db = new DbSPTD())
            {
                Pessoa pessoa = db.Pessoa.Find(id);
                db.Pessoa.Remove(pessoa);
                db.SaveChanges();
            }
        }
    }
    
}
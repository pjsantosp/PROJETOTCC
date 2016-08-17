using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SISPTD.BO
{
    public class CrudComumEntity<TEntidade, TChave>:ICRUDGenerico<TEntidade, TChave>
        where TEntidade : class
    {
        protected DbContext _contexto;
        public CrudComumEntity(DbContext contexto)
        {
            _contexto = contexto;

        }
        public IEnumerable<TEntidade> Selecionar()
        {
            try
            {
                return _contexto.Set<TEntidade>().AsEnumerable();
            }
            catch (Exception e)
            {

                throw new Exception("Erro em CrudComum Selecionar ! "+ e.Message);
            }
           
        }

        public TEntidade SelecionarPorId(TChave id)
        {
            try
            {
                return _contexto.Set<TEntidade>().Find(id);
            }
            catch (Exception e)
            {

                throw new Exception("Erro em CrudComum SelecionarPorId ! " +e.Message);
            }
            
        }

        public virtual void Inserir(TEntidade entidade)
        {
            try
            {
                _contexto.Set<TEntidade>().Add(entidade);
                _contexto.SaveChanges();

            }
            catch (Exception e)
            {
                
                throw new Exception("Erro em CrudComum Inserir " + e.Message);
            }
            
        }
        public virtual void InserirLista(List<TEntidade> Lista)
        {
            _contexto.Set<TEntidade>().AddRange(Lista);
            _contexto.SaveChanges();
        }

        public virtual void  Alterar(TEntidade entitade)
        {
            try
            {
                _contexto.Set<TEntidade>().Attach(entitade);
                _contexto.Entry(entitade).State = EntityState.Modified;
                _contexto.SaveChanges();
            }
            catch (Exception e)
            {
                
                throw new Exception("Erro em CrudComum Alterar " + e.Message);
            }
           
        }

        public void Excluir(TEntidade entidade)
        {
            try
            {
                _contexto.Set<TEntidade>().Attach(entidade);
                _contexto.Entry(entidade).State = EntityState.Deleted;
                _contexto.SaveChanges();
            }
            catch (Exception e)
            {
                
                throw new Exception("Erro em CrudComum Excluir ! " + e.Message);
            }
           
        }

        public void ExcluirPorId(TChave id)
        {
            try
            {
                TEntidade entidade = SelecionarPorId(id);
                Excluir(entidade);
            }
            catch (Exception e)
            {
                
                throw new Exception("Erro em CrudComum ExcluirPorId " + e.Message);
            }
            
            
        }
    }
}
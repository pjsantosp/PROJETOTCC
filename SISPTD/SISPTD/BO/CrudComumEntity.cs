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
            return _contexto.Set<TEntidade>().AsEnumerable();
        }

        public TEntidade SelecionarPorId(TChave id)
        {
            return _contexto.Set<TEntidade>().Find(id);
        }

        public virtual void Inserir(TEntidade entidade)
        {
            _contexto.Set<TEntidade>().Add(entidade);
            _contexto.SaveChanges();
        }

        public virtual void  Alterar(TEntidade entitade)
        {
            _contexto.Set<TEntidade>().Attach(entitade);
            _contexto.Entry(entitade).State = EntityState.Modified;
            _contexto.SaveChanges();
        }

        public void Excluir(TEntidade entidade)
        {
            _contexto.Set<TEntidade>().Attach(entidade);
            _contexto.Entry(entidade).State = EntityState.Deleted;
            _contexto.SaveChanges();
        }

        public void ExcluirPorId(TChave id)
        {
            TEntidade entidade = SelecionarPorId(id);
            Excluir(entidade);
        }
    }
}
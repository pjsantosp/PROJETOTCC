using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public interface ICRUDGenerico<TEntidade, TChave>
        where TEntidade : class
    {
        IEnumerable<TEntidade> Selecionar();
        TEntidade SelecionarPorId();
        void Inserir(TEntidade Entidade);
        void Alterar(TEntidade Entidade);
        void Excluir(TEntidade Entidade);
        void ExcluirPorId(TChave Id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISPTD.BO
{
    public interface ICRUDGenerico<TEntidade, TChave>
        where TEntidade : class
    {
        IEnumerable<TEntidade> Selecionar();
        TEntidade SelecionarPorId(TChave id);
        void Inserir(TEntidade entidade);
        void Alterar(TEntidade entitade);
        void Excluir(TEntidade entidade);
        void ExcluirPorId(TChave id);

    }
}

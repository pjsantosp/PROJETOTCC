using System.Collections.Generic;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;
using System.Data.Entity;




namespace SISPTD.BO
{
    public class DistribProcessoBO : CrudComumEntity<DistribProcesso, long>
    {
        public DistribProcessoBO(dbSISPTD contexto)
            : base(contexto)
        {

        }
        public override void Inserir(DistribProcesso entidade)
        {
            if (entidade.pessoaId == 0)
                throw new Exception("É necessario um Paciente!");
            if (entidade.SetorOrigemId == entidade.SetorDestinoId)
                throw new Exception("Setor de Origem e Destino, não pode ser iguais");
            base.Inserir(entidade);

        }
        public IEnumerable<DistribProcesso> ObterProcesso(string busca)
        {
            try
            {
                IEnumerable<DistribProcesso> listaProcesso = _contexto.Set<DistribProcesso>()
               .Include(d => d.Pessoa)
               .Where(x => x.Pessoa.cpf.Contains(busca));
                return listaProcesso;
            }
            catch (Exception e)
            {

                throw new Exception("Erro na busca na lista de pessoa", e);
            }

        }

    }
}
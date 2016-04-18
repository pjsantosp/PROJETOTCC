using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SISPTD.Models;




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

    }
}
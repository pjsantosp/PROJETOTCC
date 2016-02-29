namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DistribProcesso")]
    public  class DistribProcesso
    {
       [Key]
        public long distrib_ProcessoId { get; set; }
      
        public long? SetorOrigemId { get; set; }
      
        public long SetorDestinoId { get; set; }

        public string observacoes { get; set; }

        public long pessoaId { get; set; }

        public long usuarioEnviouId { get; set; }

        public long? usuarioRecebeuId { get; set; }

        public long? periciaId { get; set; }


        public virtual Pericia Pericia { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual Setor SetorOrigem { get; set; }

        public virtual Setor SetorDestino { get; set; }

        public virtual User UserEnviou { get; set; }

        public virtual User UserRecebeu { get; set; }
    }
}

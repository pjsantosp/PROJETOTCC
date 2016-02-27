namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    public  class DistribProcesso
    {
        [Key]
        public long distrib_ProcessoId { get; set; }

        public long? origem { get; set; }

        public long destino { get; set; }

        public string observacoes { get; set; }

        public long fk_PessoaId { get; set; }

        public long usuarioEnviou { get; set; }

        public long? fk_Pericia { get; set; }

        public long? usuarioRecebeu { get; set; }

        public virtual Pericia Pericia { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual Setor Setor { get; set; }

        public virtual Setor Setor1 { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}

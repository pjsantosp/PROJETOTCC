using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SISPTD.Models
{
    public class Movimentacao
    {
        public long movimentacaoId { get; set; }
        public long usuarioEnviouId { get; set; }
        public long? usuarioRecebeuId { get; set; }
        public long setorEnviouId { get; set; }
        public long? setorRecebeuId { get; set; }
        public long? ProcessoId { get; set; }
        public DateTime dtEnvio { get; set; }
        public DateTime ? dtRecebimento { get; set; }
        public virtual User UsuarioEnviou { get; set; }
        public virtual User UsuarioRecebeu { get; set; }
        public virtual Setor SetorEnviou{ get; set; }
        public virtual Setor SetorRecebeu { get; set; }
        public virtual Processo Processo { get; set; }
    }
}
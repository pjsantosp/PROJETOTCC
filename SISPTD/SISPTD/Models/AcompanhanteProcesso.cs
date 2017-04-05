using System.ComponentModel.DataAnnotations;

namespace SISPTD.Models
{
    public class AcompanhanteProcesso
    {
        [Key]
        public int pessoaTipoId { get; set; }
        public long? pessoaId { get; set; }
        public long? processoId { get; set; }
        public virtual Pessoa Acompanhante { get; set; }
        public virtual Processo Processo { get; set; }
    }
}
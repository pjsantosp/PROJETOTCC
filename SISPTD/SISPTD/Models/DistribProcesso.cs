namespace SISPTD.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DistribProcesso")]
    public  class DistribProcesso
    {
       [Key]
        public long distrib_ProcessoId { get; set; }
        [Display(Name="Setor de Origem")]
        public long? SetorOrigemId { get; set; }
        [Display(Name="Setor de Destino")]
        [Required(ErrorMessage = " Setor de Destino � Campo Obrigatorio! ")]
      
        public long SetorDestinoId { get; set; }
        [Display(Name="Observa��es")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = " Observa��es � Campo Obrigatorio! ")]
        public string observacoes { get; set; }
       
        public long pessoaId { get; set; }

        public long usuarioEnviouId { get; set; }

        public long? usuarioRecebeuId { get; set; }
        
        public virtual Pessoa Pessoa { get; set; }

        public virtual Setor SetorOrigem { get; set; }

        public virtual Setor SetorDestino { get; set; }

        public virtual User UserEnviou { get; set; }

        public virtual User UserRecebeu { get; set; }
    }
}

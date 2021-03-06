namespace SISPTD.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public class User
    {

        [Key]
        public long usuarioId { get; set; }
        public long setorId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Login")]
        public string login { get; set; }

        
        [Display(Name = "Senha")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string senha { get; set; }

        public long pessoaId { get; set; }

        public virtual ICollection<Agendamento> Agendamento { get; set; }

        public virtual ICollection<Movimentacao> ListaDeEnvios { get; set; }

        public virtual ICollection<Movimentacao> ListaDeRecebidos { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual Perfil Perfil { get; set; }

        public virtual ICollection<Requisicao> Requisicao { get; set; }
        public virtual Setor Setor { get; set; }

    }
}

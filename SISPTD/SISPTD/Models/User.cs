namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public  class User
    {

      [Key]
        public long usuarioId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Login")]
        public string login { get; set; }
        [Display(Name="Senha")]
        [Required]
        [StringLength(50)]
        public string senha { get; set; }

        public long pessoaId { get; set; }

        public virtual ICollection<Agendamento> Agendamento { get; set; }

        public virtual ICollection<DistribProcesso> DistribProcesso { get; set; }

        public virtual ICollection<DistribProcesso> DistribProcesso1 { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual ICollection<Requisicao> Requisicao { get; set; }
    }
}

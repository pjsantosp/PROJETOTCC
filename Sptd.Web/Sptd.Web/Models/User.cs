namespace Sptd.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public User()
        {
            Agendamento = new HashSet<Agendamento>();
            DistribProcesso = new HashSet<DistribProcesso>();
            DistribProcesso1 = new HashSet<DistribProcesso>();
            Requisicao = new HashSet<Requisicao>();
        }

        [Key]
        public long usuarioId { get; set; }

        [Required]
        [StringLength(50)]
        public string login { get; set; }

        [Required]
        [StringLength(50)]
        public string senha { get; set; }

        public long fk_PessoaId { get; set; }

        public virtual ICollection<Agendamento> Agendamento { get; set; }

        public virtual ICollection<DistribProcesso> DistribProcesso { get; set; }

        public virtual ICollection<DistribProcesso> DistribProcesso1 { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual ICollection<Requisicao> Requisicao { get; set; }
    }
}

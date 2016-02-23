namespace Sptd.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Endereco")]
    public partial class Endereco
    {
        public Endereco()
        {
            Pessoa = new HashSet<Pessoa>();
        }

        public long enderecoId { get; set; }

        public int fk_CidadeId { get; set; }

        [Required]
        [StringLength(150)]
        public string rua { get; set; }

        [Required]
        [StringLength(5)]
        public string numero { get; set; }

        [StringLength(8)]
        public string cep { get; set; }

        [StringLength(10)]
        public string bairro { get; set; }

        public virtual Cidades Cidades { get; set; }

        public virtual ICollection<Pessoa> Pessoa { get; set; }
    }
}

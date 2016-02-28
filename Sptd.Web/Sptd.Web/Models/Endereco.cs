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
            
        }
       
        public long enderecoId { get; set; }
        [Display(Name = "Cidade")]
        public int fk_CidadeId { get; set; }
        public long? fk_PessoaId { get; set; }

        [Display(Name = "Rua")]
        [Required]
        [StringLength(150)]
        public string rua { get; set; }
        [Display(Name = "Numero")]
        [Required]
        [StringLength(5)]
        public string numero { get; set; }
        [Display(Name = "CEP")]
        [StringLength(8)]
        public string cep { get; set; }
        [Display(Name="Bairro")]
        [StringLength(10)]
        public string bairro { get; set; }

        public virtual Cidades Cidades { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        
    }
}

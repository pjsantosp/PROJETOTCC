using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SISPTD.Models
{
    public class Endereco
    {
        [Key]
        public int enderecoId { get; set; }
        public int? IdCidade { get; set; }

        [StringLength(15)]
        [Display(Name = "CEP")]
        public string cep { get; set; }

        [Required(ErrorMessage = "Campo de Preenchimento Obrigatório")]
        [StringLength(100)]
        [Display(Name = "Rua")]
        public string rua { get; set; }

        [Required(ErrorMessage = "Campo de Preenchimento Obrigatório")]
        [Display(Name = "Numero")]
        public int numero { get; set; }
        
        [Required(ErrorMessage = "Campo de Preenchimento Obrigatório")]
        [StringLength(100)]
        [Display(Name = "Bairro")]
        public string bairro { get; set; }

        public virtual Cidades Cidade { get; set; }
        public virtual ICollection<Pessoa> Pessoa { get; set; }
    }
}
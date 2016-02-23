using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sptd.Web.Models.Annotations
{
    public class PessoaAnnotation
    {
       
        [Key]
        public long pessoaId { get; set; }
          
        //[ForeignKey("Endereco")]
        public Nullable<long> fk_Endereco { get; set; }
        public Nullable<System.DateTime> dt_Cadastro { get; set; }
        [Display(Name="CPF")]
        [Required(ErrorMessage="Campo de Preenchimento Obrigatório")]
        public string cpf { get; set; }
        [Required(ErrorMessage="Campo de Preenchimento Obrigatório")]
        [Display(Name="CNS")]
        public string cns { get; set; }
         [Display(Name = "RG")]
        public string rg { get; set; }
        [Required(ErrorMessage = "Campo de Preenchimento Obrigatório")]
        [Display(Name="Nome")]
        public string nome { get; set; }
        [Required(ErrorMessage = "Campo de Preenchimento Obrigatório")]

        [Display(Name="Data de Nascimento")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=true)]

        public System.DateTime dt_Nascimento { get; set; }
        [Display(Name="E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Display(Name="Mãe")]
        public string nome_Mae { get; set; }
        [Display(Name="Pai")]
        public string nome_Pai { get; set; }
        [Display(Name="Telefone")]
        [Required(ErrorMessage = "Campo de Preenchimento Obrigatório")]
        [DataType(DataType.PhoneNumber)]
        public string tel { get; set; }
        [Display(Name="Celular")]
        [DataType(DataType.PhoneNumber)]
        public string cel { get; set; }

        public virtual ICollection<Agendamento> Agendamento { get; set; }
        public virtual ICollection<DistribProcesso> DistribProcesso { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual ICollection<ItemRequisicao> ItemRequisicao { get; set; }
        public virtual ICollection<Medico> Medico { get; set; }
        public virtual ICollection<Pericia> Pericia { get; set; }
        public virtual ICollection<Pessoa> Pessoa1 { get; set; }
        public virtual Pessoa Pessoa2 { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
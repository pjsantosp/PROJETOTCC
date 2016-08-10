namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Pessoa")]
    public class Pessoa
    {
        public Pessoa()
        {
            //this.RequisicaoComoAcompanhante = new HashSet<Requisicao>();
        }
        [Key]
        public long pessoaId { get; set; }
        public long? pessoaPai { get; set; }
        public int? tipo { get; set; }

        [Display(Name = "Data Cadastro")]
        public DateTime? dt_Cadastro { get; set; }
       
        [StringLength(25)]
        [Display(Name = "CPF")]
        public string cpf { get; set; }

        [StringLength(8)]
        [Display(Name = "Crm")]
        public string crm { get; set; }
        [Required]
        [StringLength(25)]
        [Display(Name = "Cartão SUS")]
        public string cns { get; set; }

        [StringLength(25)]
        [Display(Name = "RG")]
        public string rg { get; set; }

        [StringLength(10)]
        [Display(Name = "Orgão Emissor")]
        public string orgaoemissor { get; set; }

        [Display(Name = "Data de Emissão")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? dt_Emissao { get; set; }

        [Required(ErrorMessage = "Campo de Preenchimento Obrigatório")]
        [StringLength(160)]
        [Display(Name = "Nome")]
        public string nome { get; set; }

       
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime dt_Nascimento { get; set; }

        [Display(Name ="Idade")]
        public int? idade { get; set; }

        [StringLength(50)]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

       
        [StringLength(150)]
        [Display(Name = "Mãe")]
        public string nome_Mae { get; set; }

        
        [StringLength(150)]
        [Display(Name = "Pai")]
        public string nome_Pai { get; set; }

        [StringLength(20)]
        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Campo de Preenchimento Obrigatório")]
        [DataType(DataType.PhoneNumber)]
        public string tel { get; set; }

        [StringLength(20)]
        [Display(Name = "Celular")]
        [DataType(DataType.PhoneNumber)]
        public string cel { get; set; }

        public virtual ICollection<Agendamento> Agendamento { get; set; }
        public virtual ICollection<DistribProcesso> DistribProcesso { get; set; }
        public virtual ICollection<SolicitacaoPericia> PericiaPaciente { get; set; }
        public virtual ICollection<SolicitacaoPericia> PericiaMedico { get; set; }
        //public virtual ICollection<Pessoa> PessoaAcompanhante { get; set; }
        public virtual Pessoa Pessoa_Pai { get; set; }
        public virtual ICollection<User> User { get; set; }
        public virtual ICollection<Especialidade> Especialidade { get; set; }
        //public virtual ICollection<Requisicao> RequisicaoComoPaciente { get; set; }
        public virtual ICollection<Requisicao> RequisicaoComoAcompanhante { get; set; }
        public virtual Endereco Endereco { get; set; }
       
    }
}

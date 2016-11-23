namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Classe responsável pelos dados de Pessoa 
    /// </summary>
    public class Pessoa
    {
        public Pessoa()
        {
        }
        [Key]
        public long pessoaId { get; set; }
        public long? acompanhanteId { get; set; }
        //public int? tipo { get; set; }

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

        public virtual TipoPessoa TipoPessoa { get; set; }
        public virtual ICollection<Processo> ListaDeProcessosPaciente { get; set; }
        public virtual ICollection<Processo> ListaDeProcessosMedico { get; set; }

        public virtual ICollection<Pericia> PericiaPaciente { get; set; }
        public virtual ICollection<Pericia> PericiaMedico { get; set; }
        public virtual Pessoa Acompanhante { get; set; }
        public virtual ICollection<User> Usuarios { get; set; }
        public virtual ICollection<Especialidade> Especialidade { get; set; }
        public virtual ICollection<Requisicao> RequisicaoComoPaciente { get; set; }
        public virtual List<PessoaRequisicao>PessoaRequisicao { get; set; }
        public virtual Endereco Endereco { get; set; }
       
    }
}

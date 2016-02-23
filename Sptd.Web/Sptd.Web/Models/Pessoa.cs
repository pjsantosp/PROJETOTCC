namespace Sptd.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pessoa")]
    public partial class Pessoa
    {
        public Pessoa()
        {
            Agendamento = new HashSet<Agendamento>();
            DistribProcesso = new HashSet<DistribProcesso>();
            ItemRequisicao = new HashSet<ItemRequisicao>();
            Medico = new HashSet<Medico>();
            Pericia = new HashSet<Pericia>();
            Pessoa1 = new HashSet<Pessoa>();
            User = new HashSet<User>();
        }

        public long pessoaId { get; set; }

        public long? pessoaPai { get; set; }

        public long? fk_Endereco { get; set; }
        [Display(Name="Data Cadastro")]
        public DateTime? dt_Cadastro { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name="CPF")]
        public string cpf { get; set; }

        [StringLength(25)]
        [Display(Name="Cartão SUS")]
        public string cns { get; set; }

        [StringLength(25)]
        [Display(Name="RG")]
        public string rg { get; set; }
        [StringLength(10)]
        [Display(Name="Orgão Emissor")]
        public string orgaoemissor { get; set; }
        [Display(Name = "Data de Emissão")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? dt_Emissao { get; set; }
        [Required]
        [StringLength(160)]
        [Display(Name="Nome")]
        public string nome { get; set; }
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime dt_Nascimento { get; set; }

        [StringLength(50)]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Mãe")]
        public string nome_Mae { get; set; }

        [Required]
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

        public virtual Endereco Endereco { get; set; }

        public virtual ICollection<ItemRequisicao> ItemRequisicao { get; set; }

        public virtual ICollection<Medico> Medico { get; set; }

        public virtual ICollection<Pericia> Pericia { get; set; }

        public virtual ICollection<Pessoa> Pessoa1 { get; set; }

        public virtual Pessoa Pessoa2 { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}

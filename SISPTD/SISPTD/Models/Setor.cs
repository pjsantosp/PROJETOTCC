namespace SISPTD.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Setor")]
    public  class Setor
    {
        [Key]
        public long setorId { get; set; }
        public long? usuarioId { get; set; }



        [StringLength(25)]
        [Display(Name="Nome do Setor")]
        public string descricao { get; set; }
        [StringLength(25)]
        [Display(Name ="Abreviação")]
        public string abreviacao { get; set; }


        public virtual ICollection<Movimentacao> ListaDeProcessosEnviados { get; set; }

        public virtual ICollection<Movimentacao> ListaDeProcessosRecebidos { get; set; }

        public virtual ICollection<User> ListaDeUsuarios { get; set; }
        public virtual Processo Processo { get; set; }


    }
}

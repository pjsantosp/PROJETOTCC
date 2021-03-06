namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    //[Table("Agendamento")]
    public class Agendamento
    {
        //[Key]
        public long agendamentoId { get; set; }
       public long processoId { get; set; }
        public long? usuarioId { get; set; }
        public long? clinicaId { get; set; }

        [Display(Name ="Observações")]
        [DataType(DataType.MultilineText)]
        public string observacoes { get; set; }

        [Display(Name ="Data do Agendamento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime dt_Agendamento { get; set; }
          [Display(Name = "Marcado")]
        public DateTime  dt_Marcacao { get; set; }
        public virtual User Usuarios { get; set; }
        public virtual Clinica Clinica { get; set; }
        public virtual Processo Processo { get; set; }

    }
}

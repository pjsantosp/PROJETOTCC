namespace SISPTD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Cidades")]
    public  class Cidades
    {
        

        [Key]
       
        public int IdCidade { get; set; }

        [Required]
        [StringLength(100)]
        public string Cidade { get; set; }

        public int IdEstado { get; set; }

        public virtual Estado Estado { get; set; }

        public virtual ICollection<Clinica> Clinica { get; set; }

        public virtual ICollection<Endereco> Endereco { get; set; }
    }
}

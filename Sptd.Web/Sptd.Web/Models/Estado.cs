namespace Sptd.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Estado")]
    public partial class Estado
    {
        public Estado()
        {
            Cidades = new HashSet<Cidades>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdEstado { get; set; }

        [Column("Estado")]
        [Required]
        [StringLength(20)]
        public string Estado1 { get; set; }

        [Required]
        [StringLength(2)]
        public string Sigla { get; set; }

        public virtual ICollection<Cidades> Cidades { get; set; }
    }
}

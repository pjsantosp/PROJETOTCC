namespace SISPTD.Models
{
    using System.ComponentModel.DataAnnotations;

    public  class ItemRequisicao
    {
        [Key]
        public long itemId { get; set; }

        public long? requisicaoId { get; set; }

        public virtual Requisicao Requisicao { get; set; }
       
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eStoreClient_HE163971.Models
{
    public class OrderDetail
    {
        [ForeignKey("ProductId")]
        [Required]
        public int ProductId { get; set; }

        [ForeignKey("OrderId")]
        [Required]
        public int OrderId { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Discount { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Order? Order { get; set; }

    }
}

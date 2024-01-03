using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eStoreClient_HE163971.Models
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int OrderId { get; set; }

        [ForeignKey("MemberId")]
        [Required]
        public int MemberId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime RequiredDate { get; set; }
        [Required]
        public DateTime ShippedDate { get; set; }
        [Required]
        public decimal Freight { get; set; }

        public virtual Member? Member { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }

    }
}

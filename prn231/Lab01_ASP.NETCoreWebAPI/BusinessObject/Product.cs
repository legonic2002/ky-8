using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public  int ProductId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [ForeignKey("Category")]
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int UnitsInStock { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public virtual Category? Category { get; set; }

        
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assignment01Solution_HE163971.Models
{
    public class Member
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberId { get; set; }

        [Required]
        [MaxLength(40)]
        public string Email { get; set; }
        [Required]
        [MaxLength(40)]
        public string CompanyName { get; set; }
        [Required]
        [MaxLength(40)]
        public string City { get; set; }
        [Required]
        [MaxLength(40)]
        public string Country { get; set; }

        [Required]
        [MaxLength(40)]
        public string Password { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }


    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
    public class Administrator
    {
        [Key, ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }

        [Required]
        public DateTime AdminRegistrationTime { get; set; }

        [Required]
        [StringLength(50)]
        public string ManagedEntities { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal IssueHandlingScore { get; set; } = 0.00m;
    }
}

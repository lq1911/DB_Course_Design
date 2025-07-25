using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
    public class Customer
    {
        [Key, ForeignKey("User")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public User User { get; set; }

        [StringLength(100)]
        public string? DefaultAddress { get; set; }

        public int ReputationPoints { get; set; } = 0;

        public int IsMember { get; set; } = 0;
    }
}

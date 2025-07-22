using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models
{
    public class Customer
    {
        [Key, ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }

        [StringLength(100)]
        public string? DefaultAddress { get; set; }

        public int ReputationPoints { get; set; } = 0;

        public int IsMember { get; set; } = 0;
    }

}
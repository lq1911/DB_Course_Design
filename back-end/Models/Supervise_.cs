using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
    public class Supervise_
    {
        [Key, Column(Order = 0)]
        public int AdminID { get; set; }
        [ForeignKey("AdminID")]
        public Administrator Admin { get; set; }

        [Key, Column(Order = 1)]
        public int PenaltyID { get; set; }
        [ForeignKey("PenaltyID")]
        public StoreViolationPenalty Penalty { get; set; }
    }
}

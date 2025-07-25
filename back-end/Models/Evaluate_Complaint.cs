using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
    public class Evaluate_Complaint
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminID { get; set; }
        [ForeignKey("AdminID")]
        public Administrator Admin { get; set; }

        [Key, Column(Order = 1)]
        public int ComplaintID { get; set; }
        [ForeignKey("ComplaintID")]
        public DeliveryComplaint Complaint { get; set; }
    }
}

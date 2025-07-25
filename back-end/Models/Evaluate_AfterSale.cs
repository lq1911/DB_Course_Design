using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
    public class Evaluate_AfterSale
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminID { get; set; }
        [ForeignKey("AdminID")]
        public Administrator Admin { get; set; }

        [Key, Column(Order = 1)]
        public int ApplicationID { get; set; }
        [ForeignKey("ApplicationID")]
        public AfterSaleApplication Application { get; set; }
    }
}

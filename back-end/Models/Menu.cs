using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuID { get; set; }

        [Required]
        [StringLength(50)]
        public string Version { get; set; }

        [Required]
        public DateTime ActivePeriod { get; set; }

        [Required]
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store Store { get; set; }
    }
}

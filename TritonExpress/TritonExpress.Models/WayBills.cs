using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TritonExpress.Models
{
    [Table("WayBills")]
    public class WayBills
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Weight { get; set; }

        [Required]
        public int Quantity { get; set; }


        [ForeignKey("Status")]
        public int StatusId { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
    }
}

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

        public DateTime RowCreateDate { get; set; }

        [ForeignKey("Parcel")]
        public int ParcelId { get; set; }


        [ForeignKey("Status")]
        public int StatusId { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
    }
}

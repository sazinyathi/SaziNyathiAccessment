using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TritonExpress.Models
{
    [Table("Vehicle")]
    public class Vehicle
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Model { get; set; }

        [Required]
        [MaxLength(200)]
        public string Make { get; set; }

        [Required]
        [MaxLength(200)]
        public string RegistrationNumber { get; set; }
        [Required]
        public bool IsDeleted { get; set; }


        [ForeignKey("Branches")]
        public int BranchesId { get; set; }

        [ForeignKey("VehicleType")]
        public int VehicleTypeId { get; set; }

        public WayBills WayBills { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TritonExpress.Models
{
    [Table("VehiclesType")]
    public class VehicleType
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Descriptions { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
        public IEnumerable<Vehicle> Vehicle { get; set; }
    }
}

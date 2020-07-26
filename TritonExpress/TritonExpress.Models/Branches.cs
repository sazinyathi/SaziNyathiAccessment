using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TritonExpress.Models
{
    [Table("Branches")]
    public class Branches
    {
        [Required]
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string BranchName { get; set; }

        [Required]
        [MaxLength(200)]
        public string BranchDescription { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [ForeignKey("Provinces")]
        public int ProvincesId { get; set; }

        public IEnumerable<Vehicle> Vehicle { get; set; }
    }
}

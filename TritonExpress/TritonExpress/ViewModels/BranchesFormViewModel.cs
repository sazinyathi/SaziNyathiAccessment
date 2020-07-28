using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.ViewModels
{
    public class BranchesFormViewModel
    {
      
        public int Id { get; set; }
        [Required]
        public string BranchName { get; set; }
        [Required]
        public string BranchDescription { get; set; }
        [Required]
        public string Address { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int Province { get; set; }
        public IEnumerable<Province> Provinces { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.ViewModels
{
    public class BranchesFormViewModel
    {
      
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string BranchDescription { get; set; }
        public string Address { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int Province { get; set; }
        public IEnumerable<Province> Provinces { get; set; }
    }
}

using System.Collections.Generic;
using TritonExpress.Models;

namespace TritonExpress.ViewModels
{
    public class VehicleFormViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public string RegistrationNumber { get; set; }
        public bool IsDeleted { get; set; }

        public int Branch { get; set; }
        public int VehicleType { get; set; }
      
        public IEnumerable<Branches> Branches { get; set; }
        public IEnumerable<VehicleType> VehicleTypes { get; set; }
    }
}

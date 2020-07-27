using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TritonExpress.ViewModels
{
    public class WayBillsViewModel
    {
        public string Weight { get; set; }
        public int Quantity { get; set; }
        public string CarName { get; set; }
        public string RegistrationNumber { get; set; }
        public string Status { get; set; }
        public List<WayBillsViewModel> WayBillsViewModels { get; set; } = new List<WayBillsViewModel>();
        
    }
}

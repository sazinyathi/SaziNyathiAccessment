using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.ViewModels
{
    public class WayBillsFormViewModel
    {
        public int Id { get; set; }
        public string Weight { get; set; }
        public int Quantity { get; set; }

        public int Vehicle { get; set; }
        public int Status { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
    }
}

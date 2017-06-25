using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApp.Models
{
    public class MedicineModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool AvailabilityPharmacy { get; set; }
        public decimal Price { get; set; }
        public string Company { get; set; }
        public bool Analogue { get; set; }
    }
}

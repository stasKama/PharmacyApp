using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApp.Models
{
    public class Medicine
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Field \"Name medicine\" is required")]
        [Display(Name = "Name medicine")]
        public string Name { get; set; }

        [Display(Name = "Prescription availability")]
        public bool PrescriptionMedicine { get; set; }

        [Display(Name = "Presence pharmacy")]
        public bool AvailabilityPharmacy { get; set; }

        [Required(ErrorMessage = "Field \"Dosage medicine\" is required")]
        [Display(Name = "Dosage medicine")]
        [Range(1, 10000, ErrorMessage = "Inadmissible dosage")]
        public int Dosage { get; set; }

        [Required(ErrorMessage = "Field \"Price medicine\" is required")]
        [Display(Name = "Price medicine")]
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description medicine")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Field \"Company\" is required")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Field \"International name\" is required")]
        [Display(Name = "International name")]
        public int InternationalNameId { get; set; }

        public Company Company { get; set; }
        public InternationalName InternationalName { get; set; }
    }
}

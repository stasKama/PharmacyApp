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

        [Required(ErrorMessage = "Поле \"Название лекарства\" обязательное для ввода")]
        [Display(Name = "Название лекарства")]
        public string Name { get; set; }

        [Display(Name = "Наличие рецепта")]
        public bool PrescriptionMedicine { get; set; }

        [Required(ErrorMessage = "Поле \"Дозировка лекарства\" обязательное для ввода")]
        [Display(Name = "Дозировка лекарства")]
        [Range(1, 10000, ErrorMessage = "Недопустимая дозировка")]
        public int Dosage { get; set; }

        [Required(ErrorMessage = "Поле \"Цена лекарства\" обязательное для ввода")]
        [Display(Name = "Цена лекарства")]
        public double Price { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание лекарства")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Поле \"Компания производитель\" обязательное для ввода")]
        [Display(Name = "Компания производитель")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Поле \"Международное название\" обязательное для ввода")]
        [Display(Name = "Международное название")]
        public int InternationalNameId { get; set; }

        public Company Company { get; set; }
        public InternationalName InternationalName { get; set; }
    }
}

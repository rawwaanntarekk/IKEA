﻿using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Departments
{
    public class DepartmentUpdateViewModel
    {

        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        [Display(Name = "Creation Date")]
        public DateOnly CreationDate { get; set; }
    }
}

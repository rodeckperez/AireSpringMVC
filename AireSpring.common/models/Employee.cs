using System.ComponentModel.DataAnnotations;

namespace AireSpringMVC.common.models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}")]
        public string Phone { get; set; } = null!;

        [Required]
        public int ZipCode { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Hire Date")]
        public string HireDate { get; set; }
    }
}

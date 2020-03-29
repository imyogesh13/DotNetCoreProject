using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreProject.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage ="Name field can not be empty")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage ="Invalid email format")]
        [Display(Name ="Office Email")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }

        public string PhotoPath { get; set; }

    }
}

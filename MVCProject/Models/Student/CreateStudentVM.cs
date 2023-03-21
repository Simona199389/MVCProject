using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Models.Student
{
    public class CreateStudentVM
    {
        public int Id { get; set; } = 0;
        [Required(ErrorMessage = "This field is required")]

        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int Cours { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Forma { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Specialty { get; set; }
    }
}

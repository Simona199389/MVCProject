using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Models.Classrooms
{
    public class EditVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Theme { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }
    }
}

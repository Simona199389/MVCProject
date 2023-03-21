using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    public class Classroom
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Theme { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; }
    }
}

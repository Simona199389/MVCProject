using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    public class UserToClassroom
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassroomId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(ClassroomId))]
        public Classroom Classroom { get; set; }
    }
}

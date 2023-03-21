using MVCProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Models.Classrooms
{
    public class ShareClassroomVM
    {
        public int UserId { get; set; }
        public int ClassroomId { get; set; }
        public List<User> Users { get; set; }
        public Classroom Classroom { get; set; }
        public List<UserToClassroom> Shares { get; set; }
    }
}

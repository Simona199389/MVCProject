using Microsoft.EntityFrameworkCore;
using MVCProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Repository
{
    public class MVCProjectDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<UserToClassroom> UserToClassrooms { get; set; }

       public MVCProjectDbContext()
        {
            this.Users = this.Set<User>();
            this.Classrooms = this.Set<Classroom>();
            this.UserToClassrooms = this.Set<UserToClassroom>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost; Database=MVCProjectDB; User Id=ssimeonova; Password=simonapass;"
                );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Cours = 1,
                    Forma = "Admin",
                    Specialty = "Admin"
                }
                ) ;
        }
    }
}

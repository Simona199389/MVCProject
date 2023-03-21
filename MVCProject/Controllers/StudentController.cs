using Microsoft.AspNetCore.Mvc;
using MVCProject.Entities;
using MVCProject.Extensions;
using MVCProject.Models.Student;
using MVCProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult CreateStudent()
        {

            return View();
        }
        [HttpPost]
        public IActionResult CreateStudent(CreateStudentVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User student = new User();
            student.Id = model.Id;
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.Forma = model.Forma;
            student.Password = model.Password;
            student.Username = model.Username;
            student.Specialty = model.Specialty;
            student.Cours = model.Cours;

            MVCProjectDbContext context = new MVCProjectDbContext();
            context.Users.Add(student);
            context.SaveChanges();

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
           
            MVCProjectDbContext context = new MVCProjectDbContext();
            User user = context.Users.Where(u => u.Id == id).FirstOrDefault();
            

            if (user != null)
            context.Users.Remove(user);
            context.SaveChanges();
            HttpContext.Session.Remove("loggedUser");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Profile()
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");

            MVCProjectDbContext context = new MVCProjectDbContext();

            ProfileVM model = new ProfileVM();
            model.Items = context.Users.Where(u => u.Id == loggedUser.Id).ToList();
            
            return View(model);
        }

        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            MVCProjectDbContext context = new MVCProjectDbContext();
            User user = context.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
              return RedirectToAction("Profile", "Student");
            }

            EditStudentVM editUser = new EditStudentVM();
            editUser.Id = user.Id;
            editUser.FirstName = user.FirstName;
            editUser.LastName = user.LastName;
            editUser.Username = user.Username;
            editUser.Password = user.Password;

            return View(editUser);
        }

        [HttpPost]
        public IActionResult EditStudent(EditStudentVM editUser)
        {
            if (!ModelState.IsValid)
            {
                return View(editUser);
            }
            
            MVCProjectDbContext context = new MVCProjectDbContext();
            User user = context.Users.Where(u => u.Id == editUser.Id).FirstOrDefault();

            user.Username = editUser.Username;
            user.FirstName = editUser.FirstName;
            user.LastName = editUser.LastName;
            user.Password = editUser.Password;

            HttpContext.Session.SetObject("loggedUser", user);
            context.Users.Update(user);
            context.SaveChanges();

            return RedirectToAction("Profile", "Student");

        }
    }
}

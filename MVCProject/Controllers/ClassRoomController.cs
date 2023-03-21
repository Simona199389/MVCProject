using Microsoft.AspNetCore.Mvc;
using MVCProject.Entities;
using MVCProject.Extensions;
using MVCProject.Models.Classrooms;
using MVCProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Controllers
{
    public class ClassRoomController : Controller
    {
        [HttpGet]
        public IActionResult Index(IndexVM model)
        {
            if (HttpContext.Session.GetObject<User>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");


            MVCProjectDbContext context = new MVCProjectDbContext();
            List<int> sharedClassroomsId = context.UserToClassrooms.Where(u => u.UserId == loggedUser.Id).Select(u => u.ClassroomId).ToList();
            model.classrooms = context.Classrooms.Where(c => c.OwnerId == loggedUser.Id || sharedClassroomsId.Contains(c.Id)).OrderBy(c => c.Id).ToList();

            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            MVCProjectDbContext context = new MVCProjectDbContext();
            Classroom classroom = context.Classrooms.Where(c => c.Id == id).FirstOrDefault();

            if (classroom != null)
            {
                context.Classrooms.Remove(classroom);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "ClassRoom");
        }

        [HttpGet]
        public IActionResult CreateClassroom()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateClassroom(CreateVM model)
        {
            User loggedUser = HttpContext.Session.GetObject<User>("loggedUser");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            MVCProjectDbContext context = new MVCProjectDbContext();
            Classroom classroom = new Classroom();
            classroom.OwnerId = loggedUser.Id;
            classroom.Theme = model.Theme;
            classroom.Description = model.Description;

            context.Classrooms.Add(classroom);
            context.SaveChanges();

            return RedirectToAction("Index", "ClassRoom");

        }
        [HttpGet]
        public IActionResult EditClassroom(int id)
        {
            MVCProjectDbContext context = new MVCProjectDbContext();
            Classroom classroom = context.Classrooms.Where(c => c.Id == id).FirstOrDefault();
            if (classroom == null)
            {
                return RedirectToAction("Index", "ClassRoom");
            }

            EditVM editClassroom = new EditVM();
            editClassroom.Id = classroom.Id;
            editClassroom.Theme = classroom.Theme;
            editClassroom.Description = classroom.Description;

            return View(editClassroom);
        }
        [HttpPost]
        public IActionResult EditClassroom(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            MVCProjectDbContext context = new MVCProjectDbContext();
            Classroom classroom = context.Classrooms.Where(c => c.Id == model.Id).FirstOrDefault();


            if (classroom == null)
            {
                return RedirectToAction("Index", "ClassRoom");
            }
            classroom.Theme = model.Theme;
            classroom.Description = model.Description;

            context.Classrooms.Update(classroom);
            context.SaveChanges();


            return RedirectToAction("Index", "ClassRoom");
        }
        [HttpGet]
        public IActionResult ShareClassroom(int id)
        {
            MVCProjectDbContext context = new MVCProjectDbContext();
            ShareClassroomVM model = new ShareClassroomVM();
            model.Classroom = context.Classrooms
                                    .Where(c => c.Id == id)
                                    .FirstOrDefault();
            

            model.Users = context.Users.ToList();
            model.ClassroomId = id;
            model.Shares = context.UserToClassrooms
                                    .Where(utc => utc.ClassroomId == id)
                                    .ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateShare(int UserId, int ClassroomId)
        {
            MVCProjectDbContext context = new  MVCProjectDbContext();
            UserToClassroom userToClassroom = new UserToClassroom();
            userToClassroom.UserId = UserId;
            userToClassroom.ClassroomId = ClassroomId;

            context.UserToClassrooms.Add(userToClassroom);
            context.SaveChanges();

            return RedirectToAction("Index", "ClassRoom");
        }
        [HttpGet]
        public IActionResult Members(int id)
        {
            MVCProjectDbContext context = new MVCProjectDbContext();
            ShareClassroomVM model = new ShareClassroomVM();
            model.Classroom = context.Classrooms
                                    .Where(c => c.Id == id)
                                    .FirstOrDefault();
            model.Users = context.Users.ToList();
            model.ClassroomId = id;
            model.Shares = context.UserToClassrooms
                                    .Where(utc => utc.ClassroomId == id)
                                    .ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult RevokeShare(int id)
        {
            MVCProjectDbContext context = new MVCProjectDbContext();
            UserToClassroom item = context.UserToClassrooms
                                            .Where(utc => utc.Id == id)
                                            .FirstOrDefault();
            if (item != null)
            {
                context.UserToClassrooms.Remove(item);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "ClassRoom");
        }


    }
}

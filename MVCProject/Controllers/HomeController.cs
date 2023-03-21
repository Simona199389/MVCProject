using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCProject.Entities;
using MVCProject.Extensions;
using MVCProject.Models;
using MVCProject.Models.Home;
using MVCProject.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
            public IActionResult Index()
            {
                return View();
            }

            [HttpGet]
            public IActionResult Login()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Login(LoginVM model)
            {
                if (!ModelState.IsValid)
                {
                   return View(model);
                }
            
            MVCProjectDbContext context = new MVCProjectDbContext();

            User loggedUser = context.Users.Where(s => s.Username == model.Username &&
                                            s.Password == model.Password).FirstOrDefault();

            if (loggedUser == null)
            {
                ModelState.AddModelError("authError",
                                        "Authentication failed!");
            }

            if (!ModelState.IsValid)
              return View(model);

            HttpContext.Session.SetObject("loggedUser", loggedUser);

            return RedirectToAction("Profile", "Student");

            }
            public IActionResult Logout()
            {
                HttpContext.Session.Remove("loggedUser");

                return RedirectToAction("Index", "Home");
            }
        }
    }


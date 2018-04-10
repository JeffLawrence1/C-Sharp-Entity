using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using restaurant_project.Models;
using System.Linq;

namespace restaurant_project.Controllers
{
    public class HomeController : Controller
    {
    private YourContext _context;
 
    public HomeController(YourContext context)
    {
        _context = context;
    }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // List<User> AllUsers = _context.Users.ToList();
            return View();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddReview(User newUser)
        {
            if(ModelState.IsValid){

                // User user = new User();
                // user.Name = newUser.Name;
                // user.RestaurantName = newUser.RestaurantName;
                // user.Review = newUser.Review;
                // user.Date = newUser.Date;
                // user.Stars = newUser.Stars;
                // user.createdat = DateTime.Now;
                // user.updatedat = DateTime.Now;

                _context.Users.Add(newUser);
                _context.SaveChanges();
                // int UserID = _context.Users.Last().UserID;
                // HttpContext.Session.SetInt32("UserID", UserID);
                // HttpContext.Session.SetInt32("id", (int)result[0]["id"]);

                return RedirectToAction("dash");
            }
            else{
                return View("Index");
            }
        }

        [HttpGet]
        [Route("dash")]
        public IActionResult Dash()
        {
            List<User> reviews = _context.Users.Where(r => r.Review != null).OrderByDescending(x => x.Date).ToList();
            ViewBag.reviews = reviews;
            return View("review");
        }

    }
}

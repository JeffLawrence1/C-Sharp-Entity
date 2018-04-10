using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using userDashboard_project.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace userDashboard_project.Controllers
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
            return View();
        }

        [HttpGet]
        [Route("LoginP")]
        public IActionResult LoginP(){
            return View("Login");
        }

        [HttpGet]
        [Route("Reg")]
        public IActionResult Reg(){
            return View("Register");
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel model){
            if(ModelState.IsValid){
                
                int first = 0;

                

                if(_context.Users.Count() < 1){
                    first = 9;
                }
                else{
                    first = 1;
                }
                User email = _context.Users.Where(x => x.Email == model.Email).SingleOrDefault();
                if(email == null){

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                User user = new User();
                

                user.Password = Hasher.HashPassword(user, model.Password);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.createdat = DateTime.Now;
                user.updatedat = DateTime.Now;
                user.UserLevel = first;

                _context.Users.Add(user);
                _context.SaveChanges();
                int UserID = _context.Users.Last().UserID;
                return RedirectToAction("LoginP");
                }
            
            else{
                TempData["Error"] = "Email already taken hacker!!!";
                return View("Register");
            }
        }
        return View("Register");
    }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email, string Password){

            User user = _context.Users.Where(x => x.Email == Email).SingleOrDefault();
            if(user != null && Password != null){
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, Password)){
                    HttpContext.Session.SetInt32("UserID", user.UserID);
                    return RedirectToAction("Dash", "People");
                }
            }
           
            TempData["Error"] = "Wrong email or password dummy!!!! Stop trying to hack other people's accounts!!!";
            return RedirectToAction("Login");
            
        }
}
}
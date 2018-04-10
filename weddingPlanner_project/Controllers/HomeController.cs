using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using weddingPlanner_project.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace weddingPlanner_project.Controllers
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
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel model){
            if(ModelState.IsValid){
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
                
                _context.Users.Add(user);
                _context.SaveChanges();
                int UserID = _context.Users.Last().UserID;
                return RedirectToAction("Index");
                }
            
            else{
                TempData["Error"] = "Email already taken hacker!!!";
                return View("Index");
            }
        }
        return View("Index");
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email, string Password){

            User user = _context.Users.Where(x => x.Email == Email).SingleOrDefault();
            if(user != null && Password != null){
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, Password)){
                    HttpContext.Session.SetInt32("UserID", user.UserID);
                    return Redirect("Dash");
                }
            }
           
            TempData["Error"] = "Wrong email or password dummy!!!! Stop trying to hack other people's accounts!!!";
            return RedirectToAction("Index");
            
        }
        [HttpGet]
        [Route("Dash")]
        public IActionResult Dash(){

            if(HttpContext.Session.GetInt32("UserID") == null){
                return View("Index");
            }
            
            int? ID = HttpContext.Session.GetInt32("UserID");
            

            List<Wedding> we = _context.Weddings.Where(w => w.Bride != null).Include(u => u.Attending).ThenInclude( o => o.User).ToList();
            foreach( var x in we){
                if(x.Date < DateTime.Now){
                    List<Guest> expired = _context.Guests.Where(m => m.WeddingID == x.WeddingID).ToList();
                    foreach(var y in expired){
                    _context.Guests.Remove(y);
                    _context.SaveChanges();
                    }
                    _context.Weddings.Remove(x);
                    _context.SaveChanges();
                }
            }
            List<Wedding> yy = _context.Weddings.Where(w => w.Bride != null).Include(u => u.Attending).ThenInclude( o => o.User).ToList();
            ViewBag.ID = ID;
            ViewBag.WedList = yy;
            return View("Dash");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Display/{WeddingID}")]
        public IActionResult Display(int WeddingID){
            if(HttpContext.Session.GetInt32("UserID") == null){
                return View("Index");
            }
            List<Wedding> wt = _context.Weddings.Where( y => y.WeddingID == WeddingID).Include(u => u.Attending).ThenInclude( o => o.User).ToList();

            ViewBag.info = wt;
            return View("WedDisplay");
        }
        [HttpGet]
        [Route("Delete/{WeddingID}")]
        public IActionResult Delete(int WeddingID){
            List<Guest> badd = _context.Guests.Where(m => m.WeddingID == WeddingID).ToList();
            foreach(var x in badd){
                _context.Guests.Remove(x);
                _context.SaveChanges();
            }
            Wedding bad = _context.Weddings.Where(m => m.WeddingID == WeddingID).SingleOrDefault();
            _context.Weddings.Remove(bad);
            _context.SaveChanges();
            return RedirectToAction("Dash");

        }
        [HttpGet]
        [Route("UnRsvp/{UserID}/{WeddingID}")]
        public IActionResult UnRsvp(int UserID, int WeddingID){
            Guest leaver = _context.Guests.Where(p => p.WeddingID == WeddingID).Where(u => u.UserID == UserID).SingleOrDefault();
            _context.Guests.Remove(leaver);
            _context.SaveChanges();
            return RedirectToAction("Dash");
        }
        [HttpGet]
        [Route("Rsvp/{WeddingID}")]
        public IActionResult Rsvp(int WeddingID){
            Guest drinker = new Guest();

            int? ID = HttpContext.Session.GetInt32("UserID");
            int PID = (int) ID;
            
            drinker.UserID = PID;
            drinker.WeddingID = WeddingID;

            drinker.createdat = DateTime.Now;
            drinker.updatedat = DateTime.Now;

            _context.Guests.Add(drinker);
            _context.SaveChanges();
            return RedirectToAction("Dash");
        }
    }
}

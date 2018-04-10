using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using weddingPlanner_project.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace weddingPlanner_project.Controllers
{
    public class WeddingController : Controller
    {
        private YourContext _context;

            public WeddingController(YourContext context)
                
            {
                    
            _context = context;
                
            }
            [HttpGet]
            [Route("NewW")]
            public IActionResult NewW(){
                if(HttpContext.Session.GetInt32("UserID") == null){
                return RedirectToAction("Index", "Home");
            }
                return View("NewWedding");
            }

            [HttpPost]
            [Route("NewWedding")]
            public IActionResult NewWedding(WeddingViewModel model){
                if(ModelState.IsValid){

                    Wedding w = new Wedding();

                    w.Bride = model.Bride;
                    w.Groom = model.Groom;
                    w.Date = model.Date;
                    w.Address = model.Address;
                    w.createdat = DateTime.Now;
                    w.updatedat = DateTime.Now;
                    int? ID = HttpContext.Session.GetInt32("UserID");
                    int PID = (int) ID;
                    w.UserID = PID;

                    _context.Weddings.Add(w);
                    _context.SaveChanges();

                    int WeddingID = _context.Weddings.Last().WeddingID;

                    Guest g = new Guest();

                    g.UserID = PID;
                    g.WeddingID = WeddingID;

                    g.createdat = DateTime.Now;
                    g.updatedat = DateTime.Now;

                    _context.Guests.Add(g);
                    _context.SaveChanges();


                    return RedirectToAction("Dash", "Home");
                }
                else{
                    return View("NewWedding");
                }
            }
    }
}
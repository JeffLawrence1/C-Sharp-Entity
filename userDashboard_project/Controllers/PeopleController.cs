using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using userDashboard_project.Models;

namespace userDashboard_project.Controllers
{
    public class PeopleController : Controller
    {
        private YourContext _context;
 
    

            public PeopleController(YourContext context)
                
            {
                    
            _context = context;
                
            }
        // GET: /Home/
        [HttpGet]
        [Route("Dash")]
        public IActionResult Dash()
        {
            int? ID = HttpContext.Session.GetInt32("UserID");
            return View("Dash");
        }



    }
}

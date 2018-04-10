using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace weddingPlanner_project.Models
{
    public class Guest
    {
        public int GuestID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int WeddingID { get; set; }
        public Wedding Wedding { get; set; }

        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }

        public Guest(){

           
            createdat = DateTime.Now;
            updatedat = DateTime.Now;
        }
    }
}
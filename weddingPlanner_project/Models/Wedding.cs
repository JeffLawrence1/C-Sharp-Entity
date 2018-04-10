using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace weddingPlanner_project.Models
{
    public class Wedding
    {
        public int UserID { get; set; }
        public int WeddingID { get; set; }
        public string Bride { get; set; }
        public string Groom { get; set; }

        public DateTime Date { get; set; }

        public string Address { get; set; }

        public List<Guest> Attending { get; set; }

        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }


        public Wedding(){

            Attending = new List<Guest>();
            createdat = DateTime.Now;
            updatedat = DateTime.Now;
        }
    }
}
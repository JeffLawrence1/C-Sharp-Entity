using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace userDashboard_project.Models
{
    public class Message
    {

        public int MessageID { get; set; }
        public string Mess { get; set; }

        public List<Talk> Talk { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }


        public Message(){

            Talk = new List<Talk>();
            createdat = DateTime.Now;
            updatedat = DateTime.Now;
        }
    }
    
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace userDashboard_project.Models
{
    public class Talk
    {
        public int TalkID { get; set; }
        public int MessageID { get; set; }
        public Message Message { get; set; }
        public int UserID { get; set; }
        public User User{ get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }


        public Talk(){

            createdat = DateTime.Now;
            updatedat = DateTime.Now;
        }
    }
    
}
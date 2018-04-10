using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace restaurant_project.Models
{
    public class User
    {
        public int UserID { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string RestaurantName { get; set; }

        [Required]
        [MinLength(10)]
        public string Review { get; set; }

        [Required]
        [MyDate]
        public DateTime Date { get; set; }

        [Required]
        public int Stars { get; set; }

        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }

        public User(){
            createdat = DateTime.Now;
            updatedat = DateTime.Now;
        }
    }
    
}
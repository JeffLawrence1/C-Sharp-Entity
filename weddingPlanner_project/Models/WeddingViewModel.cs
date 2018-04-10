using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace weddingPlanner_project.Models
{
    public class WeddingViewModel : BaseEntity
    {
        [Required]
        public string Bride { get; set; }

        [Required]
        public string Groom { get; set; }

        [Required]
        [MyDate]
        public DateTime Date { get; set; }

        [Required]
        public string Address { get; set; }

    }
}
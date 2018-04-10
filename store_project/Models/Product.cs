using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace store_project.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public List<Order> Order { get; set; }

        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }


        public Product(){

            Order = new List<Order>();
            createdat = DateTime.Now;
            updatedat = DateTime.Now;
        }
    }
}
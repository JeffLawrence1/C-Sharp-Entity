using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace store_project.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        public string Name { get; set; }

        public List<Order> Order { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }


        public Customer(){

            Order = new List<Order>();
            createdat = DateTime.Now;
            updatedat = DateTime.Now;
        }
    }
    
}
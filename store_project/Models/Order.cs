using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace store_project.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }


        public Order(){

            createdat = DateTime.Now;
            updatedat = DateTime.Now;
        }
    }
}
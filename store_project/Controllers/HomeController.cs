using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using store_project.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace store_project.Controllers
{
    public class HomeController : Controller
    {
        private YourContext _context;
 
    

            public HomeController(YourContext context)
                
            {
                    
            _context = context;
                
            }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Customer> newCustomers = _context.Customers.Where(x => x.CustomerID != null).ToList();
            List<Customer> newC = newCustomers.OrderByDescending(x => x.createdat).Take(3).ToList();
            List<Product> newProduct = _context.Products.Where(y => y.ProductID !=null).ToList();
            List<Product> newP = newProduct.OrderByDescending(x => x.createdat).Take(5).ToList();
            List<Order> order2 = _context.Orders.Include(y => y.Customer).Include(u => u.Product).ToList();
            List<Order> order3 = order2.OrderByDescending(x => x.createdat).Take(3).ToList();
            ViewBag.Order2 = order3;
            ViewBag.Products = newP;
            ViewBag.Customers = newC;
            return View();
        }
        [HttpGet]
        [Route("ShowAllO")]
        public IActionResult ShowAllO(){
            List<Customer> newCustomers = _context.Customers.Where(x => x.CustomerID != null).ToList();
            List<Customer> newC = newCustomers.OrderByDescending(x => x.createdat).Take(3).ToList();
            List<Product> newProduct = _context.Products.Where(y => y.ProductID !=null).ToList();
            List<Product> newP = newProduct.OrderByDescending(x => x.createdat).Take(5).ToList();
            List<Order> order2 = _context.Orders.Include(y => y.Customer).Include(u => u.Product).ToList();
            ViewBag.Order2 = order2;
            ViewBag.Products = newP;
            ViewBag.Customers = newC;
            return View("Index");
        }
        [HttpGet]
        [Route("ShowAllP")]
        public IActionResult ShowAllP(){
            List<Product> products = _context.Products.Where( x => x.ProductID != null).ToList();
            List<Customer> newCustomers = _context.Customers.Where(x => x.CustomerID != null).ToList();
            List<Customer> newC = newCustomers.OrderByDescending(x => x.createdat).Take(3).ToList();
            List<Order> order2 = _context.Orders.Include(y => y.Customer).Include(u => u.Product).ToList();
            List<Order> order3 = order2.OrderByDescending(x => x.createdat).Take(3).ToList();
            ViewBag.Order2 = order3;
            ViewBag.Products = products;
            ViewBag.Customers = newC;
            return View("Index");
        }
        [HttpGet]
        [Route("ShowAllC")]
        public IActionResult ShowAllC(){
            List<Customer> newCustomers = _context.Customers.Where(x => x.CustomerID != null).ToList();
            List<Product> newProduct = _context.Products.Where(y => y.ProductID !=null).ToList();
            List<Product> newP = newProduct.OrderByDescending(x => x.createdat).Take(5).ToList();
            List<Order> order2 = _context.Orders.Include(y => y.Customer).Include(u => u.Product).ToList();
            List<Order> order3 = order2.OrderByDescending(x => x.createdat).Take(3).ToList();
            ViewBag.Order2 = order3;
            ViewBag.Products = newP;
            ViewBag.Customers = newCustomers;
            return View("Index");
        }

        [HttpGet]
        [Route("Products")]
        public IActionResult Products(){
            List<Product> products = _context.Products.Where( x => x.ProductID != null).ToList();
            ViewBag.Products = products;
            return View("Products");
        }
        [HttpPost]
        [Route("AddP")]
        public IActionResult AddP(string Name, string Image, string Description, int IQ)
        {
            Product product = new Product();
            product.Name = Name;
            product.Image = Image;
            product.Description = Description;
            product.Quantity = IQ;
            product.createdat = DateTime.Now;
            product.updatedat = DateTime.Now;
            _context.Products.Add(product);
            _context.SaveChanges();
            int ProductID = _context.Products.Last().ProductID;
            return RedirectToAction("Products");
        }

        [HttpGet]
        [Route("Orders")]
        public IActionResult Orders()
        {
            List<Customer> newCustomers = _context.Customers.Where(x => x.CustomerID != null).ToList();
            List<Product> products = _context.Products.Where( x => x.ProductID != null).ToList();
            List<Customer> orders = _context.Customers.Where(b => b.CustomerID != null).Include(z => z.Order).ThenInclude( y => y.Product).ToList();
            List<Order> order1 = _context.Orders.Include(y => y.Customer).Include(u => u.Product).ToList();
            List<Order> order4 = order1.OrderByDescending(x => x.createdat).ToList();
            ViewBag.Orders = orders;
            ViewBag.Products = products;
            ViewBag.Customers = newCustomers;
            ViewBag.Orders1 = order4;
            return View("Orders");
        }

        [HttpPost]
        [Route("POrder")]
        public IActionResult POrder(int CustomerID, int Quantity, int ProductID){
            Order newOrder = new Order();
            newOrder.CustomerID = CustomerID;
            newOrder.ProductID = ProductID;
            newOrder.Quantity = Quantity;
            newOrder.createdat = DateTime.Now;
            newOrder.updatedat = DateTime.Now;
            _context.Orders.Add(newOrder);
            _context.SaveChanges();
            int OrderID = _context.Orders.Last().OrderID;
            Product orderr = _context.Products.Where(x => x.ProductID == ProductID).SingleOrDefault();
            orderr.Quantity -= Quantity;
            _context.SaveChanges();

            return RedirectToAction("Orders");
        }
        [HttpGet]
        [Route("Customers")]
        public IActionResult Customers()
        {
            List<Customer> customers = _context.Customers.Where(c => c.CustomerID != null).ToList();
            ViewBag.Customers = customers;
            return View("Customers");
        }
        [HttpPost]
        [Route("AddC")]
        public IActionResult AddC(string Name){
            Customer nname = _context.Customers.Where(x => x.Name == Name).SingleOrDefault();
            if(nname == null){
                Customer customer = new Customer();
                customer.Name = Name;
                customer.createdat = DateTime.Now;
                customer.updatedat = DateTime.Now;
                _context.Customers.Add(customer);
                _context.SaveChanges();
                int CustomerID = _context.Customers.Last().CustomerID;
                return RedirectToAction("Customers");
            }
            else{
                TempData["Error"] = "Customer name already exists!!!";
                return RedirectToAction("Customers");
            }
        }
        [HttpGet]
        [Route("RemoveC/{CustomerID}")]
        public IActionResult RemoveC(int CustomerID){
            Customer remove = _context.Customers.Where(v => v.CustomerID == CustomerID).SingleOrDefault();
            _context.Customers.Remove(remove);
            _context.SaveChanges();
            return RedirectToAction("Customers");
        }
        [HttpGet]
        [Route("Settings")]
        public IActionResult Settings()
        {
            return View("Settings");
        }
    }
}

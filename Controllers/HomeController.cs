using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

// ****************************************************** GET REQUEST ********************************************** //
        // main dashboard page shows most recent products, orders, and customers //
        [HttpGet("")]
        public IActionResult Index()
        {
            List<Customer> MostRecent = dbContext.Customers.OrderByDescending(c => c.CreatedAt).Take(3).ToList();
            ViewBag.RecentOrders = dbContext.Orders.Include(o=>o.Product).Include(o=>o.Customer).OrderByDescending(o => o.CreatedAt).Take(3).ToArray();
            ViewBag.products = dbContext.Products.OrderByDescending(p => p.CreatedAt).Where(c => c.Count > 0).Take(5).ToArray();
            return View("Index", MostRecent);
        }

        // view all products and add new product form // 
        [HttpGet("/products")]
        public IActionResult Products()
        {
            ViewBag.Products = dbContext.Products.Where(c => c.Count > 0).ToArray();
            return View("Products");
        }

        // view all orders and add new order form //
        [HttpGet("/orders")]
        public IActionResult Orders()
        {
            ViewBag.customers = dbContext.Customers.ToArray();
            ViewBag.products = dbContext.Products.ToArray();
            ViewBag.orders = dbContext.Orders.ToArray();
            return View("Orders");
        }

        // view all customers and add/remove customer forms //
        [HttpGet("/customers")]
        public IActionResult Customers()
        {
            ViewBag.Customers = dbContext.Customers.ToArray();
            return View("Customers");
        }

        // Delete a customer //
        [HttpGet("delete/{CustomerId}")]
        public IActionResult Delete(int CustomerId)
        {
            Customer ToDelete = dbContext.Customers.FirstOrDefault(c => c.CustomerId == CustomerId);
            dbContext.Remove(ToDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Customers");
        }


// ****************************************************** POST REQUEST ********************************************** //  
        // Create a new product //
        [HttpPost("newproduct")]
        public IActionResult NewProduct(Product FromForm)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(FromForm);
                dbContext.SaveChanges();
                return RedirectToAction("Products");
            }
            return Products();
        }
        // Add a new customer //
        [HttpPost("newcustomer")]
        public IActionResult NewCustomer(Customer FromForm)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(FromForm);
                dbContext.SaveChanges();
                return RedirectToAction("Customers");
            }
            return Customers();
        }

        // creat a new order //
        [HttpPost("neworder")]
        public IActionResult NewOrder(Order FromForm)
        {
            if(ModelState.IsValid)
            {
                Product product = dbContext.Products.FirstOrDefault(p => p.ProductId == FromForm.ProductId);
                if(FromForm.Count > product.Count)
                {
                    ModelState.AddModelError("Count", "You have entered more than what is available");
                    return Orders();
                }
                int? newCount = (product.Count - FromForm.Count);
                Product RetrievedProduct = dbContext.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                RetrievedProduct.Count = newCount;
                RetrievedProduct.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();

                dbContext.Add(FromForm);
                dbContext.SaveChanges();
                return RedirectToAction("Orders");
            }
            return Orders();
        }

    }
}

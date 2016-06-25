using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {

        public ViewResult Index()
        {
            var customers = GetCustomers();
            return View(customers);
        }
        public IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer {Name = "Jane Smith", Id = 1 },
                new Customer {Name = "John Doe" , Id = 2},
                new Customer {Name = "Steve Has" , Id = 3}
            };

        }

        public ActionResult Details(int id)
        {
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);
            if (customer == null) return HttpNotFound();

            return View(customer);

        }
    }
}
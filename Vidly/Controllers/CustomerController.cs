using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }


        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null) return HttpNotFound();

            return View(customer);

        }

        public ActionResult New()
        {
            var viewModel = new NewCustomerViewModel
            {
                Customer = new Customer(),
                MembershipTypes = _context.MembershipTypes

            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(NewCustomerViewModel viewModel)
        {


            if (!ModelState.IsValid)
            {
                var vm = new NewCustomerViewModel
                {
                    Customer = viewModel.Customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("New", vm);
            }

            if (viewModel.Customer.Id == 0)
            {
                _context.Customers.Add(viewModel.Customer);
            }else
            {
                var customerInDb = _context.Customers.Include("MembershipType").Single(c => c.Id == viewModel.Customer.Id);

                customerInDb.Name = viewModel.Customer.Name;
                customerInDb.BirthDate = viewModel.Customer.BirthDate;
                customerInDb.MembershipTypeId = viewModel.Customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = viewModel.Customer.IsSubscribedToNewsletter;

            }
            _context.SaveChanges();
            return RedirectToAction("Index","Customer");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null) return HttpNotFound();
            var viewModel = new NewCustomerViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes
            };
            return View("New", viewModel);
        }
    }
}
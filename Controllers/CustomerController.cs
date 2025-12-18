using Microsoft.AspNetCore.Mvc;
using MVC_Demo.Models;
using MVC_Demo.Services;

namespace MVC_Demo.Controllers
{
    public class CustomerController : Controller
    {
        //Dependency Injection of CustomerService
        private readonly CustomerService _service;
        private readonly ProductService _productService;
        //Constructor Injection
        public CustomerController(CustomerService service,ProductService productService)
        {
            _service = service; // customer service
            _productService = productService;
        }

        // GET: Customer
        //Id we pass the ID show specific customer else show all customers
        //actiontype indicates whether its for Details
        public IActionResult Index(int? id, string? actionType) // 1,Details
        {
            ViewBag.ActionType = actionType; // Details
            if (id.HasValue) // Id Details has ID - specific customer details
            {
                var customer = _service.GetById(id.Value);
                return View(customer);
            }
            var customers = _service.GetAll(); // Display all customers
            return View(customers);
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _service.Add(customer);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _service.Update(customer);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult BackToList()
        {
            return RedirectToAction("Index");
        }

    }
}

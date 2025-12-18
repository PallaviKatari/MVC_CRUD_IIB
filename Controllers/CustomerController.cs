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

        //Built-in Logging Service provided by ASP.NET Core - ILogger
        //LogInformation, LogWarning, LogError - Severitylevels
        private readonly ILogger<CustomerController> _logger;
        //Constructor Injection
        public CustomerController(CustomerService service,ProductService productService,ILogger<CustomerController> logger)
        {
            _service = service; // customer service
            _productService = productService;
            _logger = logger;

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
            _logger.LogInformation("Retrieved {Count} customers", customers.Count);
            return View(customers);
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _service.Add(customer);
            }
            _logger.LogInformation("Created new customer with ID {Id}", customer.Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _service.Update(customer);
            }
            _logger.LogInformation("Updated customer with ID {Id}", customer.Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            _logger.LogInformation("Deleted customer with ID {Id}", id);
            return RedirectToAction("Index");
        }

        public IActionResult BackToList()
        {
            return RedirectToAction("Index");
        }

    }
}

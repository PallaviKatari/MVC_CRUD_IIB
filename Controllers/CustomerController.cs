using Microsoft.AspNetCore.Mvc;
using MVC_Demo.Models;
using MVC_Demo.Services;

namespace MVC_Demo.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerService _service;
        public CustomerController(CustomerService service)
        {
            _service = service;
        }

        public IActionResult Index(int? id, string? actionType)
        {
            ViewBag.ActionType = actionType;
            if (id.HasValue)
            {
                var customer = _service.GetById(id.Value);
                return View(customer);
            }
            var customers = _service.GetAll();
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

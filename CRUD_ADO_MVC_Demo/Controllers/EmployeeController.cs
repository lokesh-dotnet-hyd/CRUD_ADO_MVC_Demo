using CRUD_ADO_MVC_Demo.Services;
using Microsoft.AspNetCore.Mvc;
using CRUD_ADO_MVC_Demo.Models;

namespace CRUD_ADO_MVC_Demo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IRepository _repo;

        public EmployeeController(IRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View(_repo.GetAll());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(emp);
                return RedirectToAction("Index");
            }
            return View(emp);
        }


        public IActionResult Update(int id)
        {
            var emp = _repo.GetById(id);
            if (emp != null)
            {
                return View(emp);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var emp = _repo.GetById(employee.Id);
                if (emp != null)
                {
                    emp.Name = employee.Name;
                    emp.Department = employee.Department;
                    emp.Salary = employee.Salary;

                    _repo.Update(emp);
                    return RedirectToAction("Index");
                }
                return NotFound();
            }

            return View(employee);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var emp = _repo.GetById(id);
            if (emp == null)
                return NotFound();

            return View(emp); 
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

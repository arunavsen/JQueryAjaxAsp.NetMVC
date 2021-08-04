using JQueryAjaxAsp.NetMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JQueryAjaxAsp.NetMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController()
        {
            var context = new ApplicationDbContext();
            _context = context;
        }
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(GetAllEmployees());
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            Employee emp = new Employee();
            return View(emp);
        }

        [HttpPost]
        public ActionResult AddOrEdit()
        {
            return View();
        }
    }
}
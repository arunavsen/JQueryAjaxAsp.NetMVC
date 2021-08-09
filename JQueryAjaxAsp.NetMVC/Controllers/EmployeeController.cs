using JQueryAjaxAsp.NetMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            Employee emp = new Employee();
            if (id != 0)
            {
                emp = _context.Employees.Where(x => x.EmployeeId == id).FirstOrDefault<Employee>();
            }
            return View(emp);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Employee emp)
        {
            try
            {
                if (emp.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(emp.ImageUpload.FileName);

                    string extension = Path.GetExtension(emp.ImageUpload.FileName);

                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                    emp.ImagePath = "~/AppFiles/Images/" + fileName;

                    emp.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }
                if (emp.EmployeeId == 0)
                {
                    _context.Employees.Add(emp);
                    _context.SaveChanges();
                }
                else
                {
                    _context.Entry(emp).State = EntityState.Modified;
                    _context.SaveChanges();
                }
               

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployees()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Employee emp = _context.Employees.Where(x => x.EmployeeId == id).FirstOrDefault<Employee>();
                _context.Employees.Remove(emp);
                _context.SaveChanges();

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployees()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);                
            }
        }
    }
}
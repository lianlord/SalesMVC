using Microsoft.AspNetCore.Mvc;
using SalesMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> departments = new List<Department>()
            {
                new Department{Id = 1, Nome="Eletronics"},
                new Department{Id = 2, Nome="Tools"}
            };
            ViewData["contact"] = "dinamo@mail.com";
            return View(departments);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SalesMVC.Models;
using SalesMVC.Models.ViewModels;
using SalesMVC.Services;
using SalesMVC.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            var list = _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = list };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            //validar entrada
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };
                return View(viewModel);
            }
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(long? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            
            Seller seller = _sellerService.FindSellerAndDepartmentByIdSellerAsync(id.Value);
            if (seller == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            return View(seller);            
        }

        public IActionResult Edit(long? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});

            Seller seller = _sellerService.FindSellerAndDepartmentByIdSellerAsync(id.Value);
            if (seller == null) 
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            List<Department> departments = _departmentService.FindAllAsync();
            SellerFormViewModel sellerFormViewModel = new SellerFormViewModel { Departments = departments, Seller = seller};
            return View(sellerFormViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            //validar entrada
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };
                return View(viewModel);
            }
            if (id != seller.Id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            try
            {
                _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException ex) {
                return RedirectToAction(nameof(Error), new { ex.Message});
            }
           
        }
        public IActionResult Delete(long? id)
        {
            if (id == null)            
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            
            Seller seller = _sellerService.FindSellerByIdAsync(id.Value);
            if (seller == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel() { 
                Message = message, 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
        
    }  
}

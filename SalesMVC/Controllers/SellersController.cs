using Microsoft.AspNetCore.Mvc;
using SalesMVC.Models;
using SalesMVC.Models.ViewModels;
using SalesMVC.Services;
using SalesMVC.Services.Exceptions;
using System;
using System.Collections.Generic;
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
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var list = _departmentService.findAll();
            var viewModel = new SellerFormViewModel { Departments = list };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(long? id)
        {
            if (id == null) return NotFound();
            
            Seller seller = _sellerService.FindSellerAndDepartmentByIdSeller(id.Value);
            if (seller == null) return NotFound();
            return View(seller);            
        }

        public IActionResult Edit(long? id)
        {
            if (id == null) return NotFound();
            Seller seller = _sellerService.FindSellerAndDepartmentByIdSeller(id.Value);
            if (seller == null) return NotFound();
            List<Department> departments = _departmentService.findAll();
            SellerFormViewModel sellerFormViewModel = new SellerFormViewModel { Departments = departments, Seller = seller};
            return View(sellerFormViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if(id != seller.Id)
            {
                return BadRequest();
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException ex) {
                return NotFound();
            }
            catch(DBConcurrencyException ex)
            {
                return BadRequest();
            }
           
        }
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Seller seller = _sellerService.FindSellerById(id.Value);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        
    }  
}

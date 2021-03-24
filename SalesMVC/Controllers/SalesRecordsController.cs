using Microsoft.AspNetCore.Mvc;
using SalesMVC.Models;
using SalesMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMVC.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            DateTime now = DateTime.Now;
            if (!minDate.HasValue)
            {
                minDate = new DateTime(now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            List<SalesRecord> sales = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(sales);
        }
        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            DateTime now = DateTime.Now;
            if (!minDate.HasValue)
            {
                minDate = new DateTime(now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            List<IGrouping<Department, SalesRecord>> sales = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);
            return View(sales);
        }
    }
}

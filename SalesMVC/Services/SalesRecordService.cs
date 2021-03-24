using Microsoft.EntityFrameworkCore;
using SalesMVC.Data;
using SalesMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMVC.Services
{
    public class SalesRecordService
    {
        private readonly SalesContext _context;

        public SalesRecordService(SalesContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindSalesByDateAsync(DateTime? initial, DateTime? final)
        {
            /*
            var result = from s in _context.SalesRecord select s;
            if (initial.HasValue)
            {
                result = result.Where(s => s.Date >= initial);
            }
            if (final.HasValue)
            {
                result = result.Where(s => s.Date <= final);
            }
            return await result
                .Include(s => s.Seller)
                .Include(s => s.Seller.Department)
                .OrderBy(s => s.Date)
                .ToListAsync();
            */
            return await _context.SalesRecord
                .Where(s => s.Date >= initial && s.Date <= final)
                .OrderBy(s => s.Date)
                .Include(s => s.Seller)
                .Include(s => s.Seller.Department)
                .ToListAsync();
            
        }
    }
}

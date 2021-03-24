using Microsoft.EntityFrameworkCore;
using SalesMVC.Data;
using SalesMVC.Models;
using SalesMVC.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMVC.Services
{
    public class SellerService
    {
        private readonly SalesContext _context;

        public SellerService(SalesContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public void Insert(Seller seller)
        {             
            _context.Add(seller);
            _context.SaveChanges();
        }

        public async Task<Seller> FindSellerByIdAsync(long id)
        {
            return await _context.Seller.FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Seller> FindSellerAndDepartmentByIdSellerAsync(long id)
        {
            return await _context.Seller.Include(s => s.Department).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task RemoveAsync(long id)
        {
            try
            {
                var seller = await FindSellerByIdAsync(id);
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
            }catch(DbUpdateException ex)
            {
                throw new IntegrityException(ex.Message);
            }
           
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(s => s.Id == seller.Id);
            if(hasAny)
            {                
                try
                {
                    _context.Update(seller);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException ex)
                {
                    throw new DBConcurrencyException(ex.Message);
                }
            }
            else
            {
                throw new NotFoundException("Seller not found by id");
            }
        }
    }
}

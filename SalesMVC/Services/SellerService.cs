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

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {             
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindSellerById(long id)
        {
            return _context.Seller.FirstOrDefault(s => s.Id == id);
        }
        public Seller FindSellerAndDepartmentByIdSeller(long id)
        {
            return _context.Seller.Include(s => s.Department).FirstOrDefault(s => s.Id == id);
        }

        public void Remove(long id)
        {
            var seller = FindSellerById(id);
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }

        public void Update(Seller seller)
        {
            if(_context.Seller.Any(s => s.Id == seller.Id))
            {                
                try
                {
                    _context.Update(seller);
                    _context.SaveChanges();
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
